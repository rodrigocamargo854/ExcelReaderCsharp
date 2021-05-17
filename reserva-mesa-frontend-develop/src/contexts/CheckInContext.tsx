import React, { createContext, ReactNode, useState } from "react";
import { CheckIn } from "~/domain/models/check-in";

interface CheckInContextData {
  modalIsOpen: boolean;
  reservation: CheckIn;
  wasChanged: boolean;
  setModalIsOpen: (value: boolean) => void;
  setReservation: (value: CheckIn) => void;
  setWasChanged: (value: boolean) => void;
}

interface CheckInProviderProps {
  children: ReactNode;
}

export const CheckInContext = createContext<CheckInContextData>(
  {} as CheckInContextData
);

export function CheckInProvider({
  children,
}: CheckInProviderProps) {
  const [modalIsOpen, setModalIsOpen] = useState<boolean>(false);
  const [wasChanged, setWasChanged] = useState<boolean>(true);
  const [reservation, setReservation] = useState<CheckIn>(
    {} as CheckIn
  );

  return (
    <CheckInContext.Provider
      value={{ modalIsOpen, setModalIsOpen, reservation, setReservation, wasChanged, setWasChanged }}
    >
      {children}
    </CheckInContext.Provider>
  );
}