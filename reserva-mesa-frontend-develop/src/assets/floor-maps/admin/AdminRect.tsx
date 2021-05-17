import React, { useContext, useEffect, useState } from "react";

import { UpdateWorkstationContext } from "~/contexts/UpdateWorkstationContext";

import { Workstation } from "~/domain/models/workstation";

import { getWorkstationByName } from "~/services/http/backend/workstation-service";

interface AdminRectProps {
  name: string;
  height: string;
  width: string;
  y: string;
  x: string;
  strokeWidth: string;
  stroke: string;
}

function AdminRect({
  name,
  height,
  y,
  x,
  width,
  strokeWidth,
  stroke,
}: AdminRectProps) {
  const [fill, setFill] = useState<string>("#666");

  const {
    workstation,
    setWorkstation,
    modalIsOpen,
    setModalIsOpen,
  } = useContext(UpdateWorkstationContext);

  function handleClick() {
    setWorkstation({ name: name } as Workstation);
    setModalIsOpen(true);
  }

  useEffect(() => {
    (async () => {
      const response = await getWorkstationByName(name);
      setWorkstation(response.data);
      setFill(response.data.active ? "#0f0" : "#666");
    })();
  }, []);

  useEffect(() => {
    (async () => {
      if (workstation.name === undefined || workstation.name === name) {
        const response = await getWorkstationByName(name);
        setWorkstation(response.data);
        setFill(response.data.active ? "#0f0" : "#666");
      }
    })();
  }, [modalIsOpen]);

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

export default AdminRect;
