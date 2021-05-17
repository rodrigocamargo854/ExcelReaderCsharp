import React, { useContext, useEffect } from "react";
import moment, { Moment } from "moment";
import { DatePicker } from "antd";

import { SelectUnit, SelectFloor, Map, MapLegend } from "~/components";

import floors from "../../../assets/floor-maps/user/floors";
import { UserMapContext } from "~/contexts";
import { PopupStatus, showPopup } from "~/application/popup";

const CreateReservation = () => {
  const { finalDate, initialDate, setFinalDate, setInitialDate } = useContext(
    UserMapContext
  );

  const maxDate = () => {
    const today = moment();
    return today.add(7 - today.weekday(), "days").add(5, "days")
  };

  const minDate = () => {
    const date = new Date();
    return date.toISOString().split("T")[0];
  };

  const disabledDate = (current: Moment) => {
    return current < moment(minDate()) || current > moment(maxDate());
  };

  const validateDateInterval = () => {
    const initialDateFormValue = moment(initialDate);
    const finalDateFormValue = moment(finalDate);

    if (initialDateFormValue.isAfter(finalDateFormValue)) {
      showPopup({
        status: PopupStatus.Error,
        title: "A data inicial é maior do que a data final.",
        message:
          "Altere as datas para um intervalo de tempo válido para reservar a mesa.",
      });
    }
  };

  const handleInitialDateChange = (date: Moment | null) => {
    if (date) {
      setInitialDate(date.format("yyyy/MM/DD"));
    }
  };

  const handleFinalDateChange = (date: Moment | null) => {
    if (date) {
      setFinalDate(date.format("yyyy/MM/DD"));
    }
  };

  useEffect(() => {
    validateDateInterval();
  }, [initialDate, finalDate]);

  return (
    <>
      <div className="create-reservation-form">
        <div className="create-reservation-selects">
          <SelectUnit />
          <SelectFloor />
        </div>
        <div className="create-reservation-datepickers">
          <div className="initial">
            <span>Data Inicial</span>
            <DatePicker
              placeholder=""
              className="initial-datepicker"
              disabledDate={disabledDate}
              format="DD/MM/yyyy"
              onChange={(date, dateString) => handleInitialDateChange(date)}
            />
          </div>
          <div className="final">
            <span>Data Final</span>
            <DatePicker
              placeholder=""
              className="final-datepicker"
              disabledDate={disabledDate}
              format="DD/MM/yyyy"
              onChange={(date, dateString) => handleFinalDateChange(date)}
            />
          </div>
        </div>
      </div>

      {initialDate && finalDate ? (
        <div className="create-reservation-map">
          <MapLegend />
          <Map floors={floors}/>
        </div>
      ) : <></>}
    </>
  );
};

export default CreateReservation;
