import React, { createContext, ReactNode, useState } from 'react';

import { Floor } from "~/domain/models/floor"
import { getFloorsByUnitId } from '~/services/http/backend/floor-service';

interface FloorsContextData {
  modalIsOpen: boolean;
  selectedFloor?: Floor;
  unitId?: number;
  floors?: Floor[];
  setSelectedFloor: (value: Floor | undefined) => void;
  setModalIsOpen: (value: boolean) => void;
  setUnitId: (value: number) => void;
  fetchFloors: () => Promise<void>;
}

interface FloorsProvider {
  children: ReactNode;
}

export const FloorsContext = createContext<FloorsContextData>({} as FloorsContextData);

export function FloorsProvider({ children }: FloorsProvider) {
  const [unitId, setUnitId] = useState<number>();
  const [floors, setFloors] = useState<Floor[]>();
  const [modalIsOpen, setModalIsOpen] = useState<boolean>(false);
  const [selectedFloor, setSelectedFloor] = useState<Floor | undefined>();

  async function fetchFloors(): Promise<void> {
    if (unitId !== undefined) {
      const response = await getFloorsByUnitId(unitId);

      response.statusCode !== 200 ? setFloors([]) : setFloors(response.data);
    }
  }

  return (
    <FloorsContext.Provider value={{
      unitId,
      setUnitId,
      floors,
      fetchFloors,
      modalIsOpen,
      selectedFloor,
      setModalIsOpen,
      setSelectedFloor
    }}>
      { children}
    </FloorsContext.Provider>
  );
}
