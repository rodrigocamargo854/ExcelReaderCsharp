import React, { createContext, ReactNode, useState } from "react";
import { CreateReservation } from "~/domain/models/reservation";

interface CreateReservationContextData {
  workstationName?: string;
  modalIsOpen: boolean;
  reservation?: CreateReservation;
  setWorkstationName: (value: string) => void;
  setModalIsOpen: (value: boolean) => void;
  setReservation: (value: CreateReservation) => void;
}

interface CreateReservationContextProviderProps {
  children: ReactNode;
}

export const CreateReservationContext = createContext<CreateReservationContextData>({} as CreateReservationContextData);

export function CreateReservationProvider({ children }: CreateReservationContextProviderProps) {
  const [workstationName, setWorkstationName] = useState<string>();
  const [modalIsOpen, setModalIsOpen] = useState<boolean>(false);
  const [reservation, setReservation] = useState<CreateReservation>();

  return (
    <CreateReservationContext.Provider value={{ workstationName, setWorkstationName, modalIsOpen, setModalIsOpen, reservation, setReservation }}>
      { children }
    </CreateReservationContext.Provider>
  );
}
