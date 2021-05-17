import React, { useEffect, useState, useContext } from "react";
import { Modal } from "antd";

import { UserRectStatus, UserRectColor } from "./enums";
import { CreateReservationContext, UserMapContext } from "~/contexts";
import * as reservationService from "~/services/http/backend/reservation-service";
import { PopupStatus, showPopup } from "~/application/popup";

const { confirm } = Modal;

interface UserRectProps {
  name: string;
  height: string;
  width: string;
  y: string;
  x: string;
  strokeWidth: string;
  stroke: string;
  status?: UserRectStatus;
}

function UserRect({
  name,
  height,
  y,
  x,
  width,
  strokeWidth,
  stroke,
  status,
}: UserRectProps) {
  const [fill, setFill] = useState<string>(UserRectColor.available);

  const createReservationContext = useContext(CreateReservationContext);
  const userMapContext = useContext(UserMapContext);

  const openModal = (workstationName: string) => {
    confirm({
      title: "Você tem certeza de que deseja reservar esta mesa?",
      cancelText: "Cancelar",
      okText: "Reservar",
      async onOk() {
        if (
          workstationName &&
          userMapContext.finalDate &&
          userMapContext.initialDate
        ) {
          const response = await reservationService.createReservation({
            finalDate: new Date(userMapContext.finalDate),
            initialDate: new Date(userMapContext.initialDate),
            workstationName: workstationName,
          });
          const success = response.statusCode === 201;

          createReservationContext.setModalIsOpen(false);

          showPopup({
            status: success ? PopupStatus.Success : PopupStatus.Error,
            title: success
              ? "Reservas criadas com sucesso!"
              : "Houve um erro ao tentar reservar a estação de trabalho",
          })
        }
      },
      onCancel() {
        createReservationContext.setWorkstationName("");
        createReservationContext.setModalIsOpen(false);
      },
    });
  };

  useEffect(() => {
    if (status === UserRectStatus.available) {
      setFill(UserRectColor.available);
    } else if (status === UserRectStatus.disabled) {
      setFill(UserRectColor.disabled);
    } else {
      setFill(UserRectColor.reserved);
    }
  });

  function handleClick() {
    if (fill === UserRectColor.available) {
      createReservationContext.setModalIsOpen(true);

      openModal(name);
    }
  }

  return (
    <rect
      name={name}
      height={height}
      width={width}
      y={y}
      x={x}
      strokeWidth={strokeWidth}
      stroke={stroke}
      fill={fill}
      onClick={handleClick}
    />
  );
}

export default UserRect;
