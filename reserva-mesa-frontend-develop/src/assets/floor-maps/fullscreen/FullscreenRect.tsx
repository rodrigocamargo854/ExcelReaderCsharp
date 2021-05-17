import React, { useEffect, useState, useContext } from "react";
import { Modal } from "antd";

import { FullscreenRectStatus, FullscreenRectColor } from "./enums";
import { CreateReservationContext, UserMapContext } from "~/contexts";

interface FullscreenRectProps {
  name: string;
  height: string;
  width: string;
  y: string;
  x: string;
  strokeWidth: string;
  stroke: string;
  status?: FullscreenRectStatus;
}

function FullscreenRect({
  name,
  height,
  y,
  x,
  width,
  strokeWidth,
  stroke,
  status,
}: FullscreenRectProps) {
  const [fill, setFill] = useState<string>(FullscreenRectColor.available);

  const createReservationContext = useContext(CreateReservationContext);
  const userMapContext = useContext(UserMapContext);

  useEffect(() => {
    if (status === FullscreenRectStatus.available) {
      setFill(FullscreenRectColor.available);
    } else if (status === FullscreenRectStatus.disabled) {
      setFill(FullscreenRectColor.disabled);
    } else {
      setFill(FullscreenRectColor.reserved);
    }
  });

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
    />
  );
}

export default FullscreenRect;