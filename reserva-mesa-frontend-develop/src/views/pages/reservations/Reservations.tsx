import React, { useContext } from "react";
import { DatePicker, Divider, Button } from "antd";
import { AuthenticatedUserReservationsContext } from "~/contexts";
import { Moment } from "moment";
import { CloseOutlined } from "@ant-design/icons"

import { ReservationList } from "./components";
import moment from "moment";
import * as reservationService from "~/services/http/backend/reservation-service";
import { PopupStatus, showPopup } from "~/application/popup";

const Reservations = () => {
  const {
    selectedReservationsId,
    reservations,
    initialDate,
    finalDate,
    setInitialDate,
    setFinalDate,
    setSelectedReservationsId,
    fetchReservations
  } = useContext(AuthenticatedUserReservationsContext);

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

  const removeSelectedReservationsOutsideDateInterval = async (): Promise<void> => {
    selectedReservationsId.forEach(reservationId => {
      const reservation = reservations.find(r => r.id === reservationId);

      const isAfter = moment(reservation?.date).isAfter(finalDate);
      const isBefore =  moment(reservation?.date).isBefore(initialDate);

      if (isAfter || isBefore) {
        const index = selectedReservationsId.indexOf(reservationId);
        selectedReservationsId.splice(index, 1);
        setSelectedReservationsId(selectedReservationsId);
      }
    });
  }

  const handleCancel = async () => {
    if (!selectedReservationsId.length) {
      return;
    }

    await removeSelectedReservationsOutsideDateInterval();

    const response = await reservationService.cancelReservations(selectedReservationsId);
    if (response.statusCode !== 204) {
      return showPopup({
        title: 'Não foi possível cancelar suas reservas.',
        status: PopupStatus.Error
      });
    }

    showPopup({
      title: 'Reservas canceladas.',
      status: PopupStatus.Success
    });
    setSelectedReservationsId([]);
    await fetchReservations();
  }

  return (
    <div className="reservations-wrapper">
      <header className="reservations">
        <div>
          <span>Data Inicial</span>
          <DatePicker
            placeholder=""
            className="initial-datepicker"
            style={{ width: "100%" }}
            format="DD/MM/yyyy"
            onChange={(date, dateString) => handleInitialDateChange(date)}
          />
        </div>
        <div>
          <span>Data Final</span>
          <DatePicker
            placeholder=""
            className="initial-datepicker"
            style={{ width: "100%" }}
            format="DD/MM/yyyy"
            onChange={(date, dateString) => handleFinalDateChange(date)}
          />
        </div>
      </header>
      <div className="reservations-buttons">
        <Button icon={<CloseOutlined />} onClick={handleCancel} >Cancelar</Button>
      </div>
      <Divider />
      <ReservationList />
    </div>
  );
}

export default Reservations;
