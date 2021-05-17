import React, { useState } from "react";
import { Button, DatePicker, Divider } from "antd";
import moment, { Moment } from "moment";
import FileSaver from "file-saver";

import { PopupStatus, showPopup } from "~/application/popup";
import { getAllConfirmedWorkstationsFromDateInterval, getAllReservedWorkstationsFromDateInterval } from "~/services/http/backend/reservation-service";
import { EmitReportTabs } from "../enums";

interface EmitReportTabContentProps {
  report: EmitReportTabs;
}

const EmitReportTabContent = ({ report }: EmitReportTabContentProps) => {
  const [initialDate, setInitialDate] = useState<string>("");
  const [finalDate, setFinalDate] = useState<string>("");

  const handleInitialDateChange = (date: Moment | null): void => {
    if (date) {
      setInitialDate(date.format("yyyy/MM/DD"));
    }
  }

  const handleFinalDateChange = (date: Moment | null): void => {
    if (date) {
      setFinalDate(date.format("yyyy/MM/DD"));
    }
  }

  const dateIntervalIsValid = (): boolean => {
    const initialDateFormValue = moment(initialDate);
    const finalDateFormValue = moment(finalDate);
    return initialDateFormValue.isBefore(finalDateFormValue);
  };

  const handleButtonClick = async (): Promise<void> => {
    if (!dateIntervalIsValid()) {
      return showPopup({
        status: PopupStatus.Error,
        title: "A data inicial é maior do que a data final.",
        message: "Altere as datas para um intervalo de tempo válido para reservar a mesa.",
      });
    }

    await fetchDataAndGenerateFile();
  };

  const generateReportFileName = (): string =>
    `relatorio-${moment().format('DD-MM-YYYY')}.csv`;

  const generateFile = (data: any): void => {
    const blob = new Blob([data], {type: "text/csv;charset=utf-8"});
    FileSaver.saveAs(blob, generateReportFileName());
  }

  const fetchDataAndGenerateFile = async (): Promise<void> => {
    let response;

    if (report === EmitReportTabs.ReservedWorkstations) {
      response = await getAllReservedWorkstationsFromDateInterval(initialDate, finalDate);
    } else if(report === EmitReportTabs.ConfirmedWorkstations){
      response = await getAllConfirmedWorkstationsFromDateInterval(initialDate, finalDate);
    }

    response && response.statusCode === 200
      ? generateFile(response.data)
      : showPopup({
        status: PopupStatus.Error,
        title: "Houve um erro ao gerar seu relatório."
      });
  }

  return (
    <div className="emit-report-tab-content">
      <div className="emit-report-datepickers">
        <div className="emit-report-left">
          <span className="emit-report-datepicker-span">Data Inicial</span>
          <DatePicker
            placeholder=""
            className="emit-report-initial-datepicker"
            format="DD/MM/yyyy"
            onChange={(date, dateString) => handleInitialDateChange(date)}
          />
        </div>
        <div className="emit-report-right">
          <span className="emit-report-datepicker-span">Data Final</span>
          <DatePicker
            placeholder=""
            className="emit-report-final-datepicker"
            format="DD/MM/yyyy"
            onChange={(date, dateString) => handleFinalDateChange(date)}
          />
        </div>
      </div>
      <Divider />
      <div className="emit-report-buttons">
        <Button onClick={handleButtonClick}>Emitir relatório</Button>
      </div>
    </div>
  );
};

export default EmitReportTabContent;
