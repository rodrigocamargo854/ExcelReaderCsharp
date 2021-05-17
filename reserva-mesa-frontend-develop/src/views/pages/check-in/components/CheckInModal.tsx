import React, { useContext } from "react";
import { Modal } from "antd";
import { CheckInContext } from "~/contexts/CheckInContext";
import { checkInReservation } from "~/services/http/backend/reservation-service"

const CheckInModal: React.FC = () => {
  const { modalIsOpen, setModalIsOpen, reservation, setWasChanged } = useContext(CheckInContext);

  const onOk = async () => {
    setModalIsOpen(false);
    checkInReservation(reservation.reservationId);
    setWasChanged(true);
  }

  const onCancel = () => {
    setModalIsOpen(false);
  }

  return (
    <Modal
      visible={modalIsOpen}
      closable={false}
      okText="Confirmar"
      cancelText="Cancelar"
      onOk={onOk}
      onCancel={onCancel}
    >
      Confirmar o check-in da reserva de: {reservation.userName} na mesa {reservation.workstationName}?
    </Modal>
  );
}

export default CheckInModal;
