import React, { createContext, ReactNode, useEffect, useState } from "react";

interface UserMapContextData {
  unitId: number;
  floorId: number;
  floorCode: string;
  initialDate: string;
  finalDate: string;
  setUnitId: (value: number) => void;
  setFloorId: (value: number) => void;
  setFloorCode: (value: string) => void;
  setInitialDate: (value: string) => void;
  setFinalDate: (value: string) => void;
}

interface UserMapProviderProps {
  children: ReactNode;
}

export const UserMapContext = createContext<UserMapContextData>(
  {} as UserMapContextData
);

export function UserMapProvider({ children }: UserMapProviderProps) {
  const [unitId, setUnitId] = useState<number>(0);
  const [floorId, setFloorId] = useState<number>(0);
  const [floorCode, setFloorCode] = useState<string>("");
  const [initialDate, setInitialDate] = useState<string>("");
  const [finalDate, setFinalDate] = useState<string>("");

  return (
    <UserMapContext.Provider
      value={{
        unitId,
        floorId,
        setUnitId,
        setFloorId,
        initialDate,
        setInitialDate,
        finalDate,
        setFinalDate,
        floorCode,
        setFloorCode,
      }}
    >
      {children}
    </UserMapContext.Provider>
  );
}
