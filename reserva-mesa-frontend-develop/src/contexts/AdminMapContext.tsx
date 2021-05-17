import React, { createContext, ReactNode, useState } from "react";

interface AdminMapContextData {
  unitId: number;
  floorId: number;
  setUnitId: (value: number) => void;
  setFloorId: (value: number) => void;
}

interface AdminMapProviderProps {
  children: ReactNode;
}

export const AdminMapContext = createContext<AdminMapContextData>(
  {} as AdminMapContextData
);

export function AdminMapProvider({ children }: AdminMapProviderProps) {
  const [unitId, setUnitId] = useState<number>(0);
  const [floorId, setFloorId] = useState<number>(0);

  return (
    <AdminMapContext.Provider
      value={{ unitId, floorId, setUnitId, setFloorId }}
    >
      {children}
    </AdminMapContext.Provider>
  );
}
