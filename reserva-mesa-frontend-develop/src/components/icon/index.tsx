import React from "react";
import * as ListIcons from "@ant-design/icons";

interface IconProps{
    name?: any;
    size?: any;
    color?: any;
}

export const Icon: React.FC<IconProps> = (props: IconProps) => {
  const ComponentIcon = (ListIcons as any)[props.name];
  return <ComponentIcon/>;
};