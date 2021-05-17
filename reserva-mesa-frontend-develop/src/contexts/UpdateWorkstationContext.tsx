import React, { createContext, ReactNode, useState } from "react";

import { Workstation } from "~/domain/models/workstation";

interface UpdateWorkstationContextData {
  modalIsOpen: boolean;
  workstation: Workstation;
  setModalIsOpen: (value: boolean) => void;
  setWorkstation: (value: Workstation) => void;
}

interface UpdateWorkstationProviderProps {
  children: ReactNode;
}

export const UpdateWorkstationContext = createContext<UpdateWorkstationContextData>(
  {} as UpdateWorkstationContextData
);

export function UpdateWorkstationProvider({
  children,
}: UpdateWorkstationProviderProps) {
  const [modalIsOpen, setModalIsOpen] = useState<boolean>(false);
  const [workstation, setWorkstation] = useState<Workstation>(
    {} as Workstation
  );

  return (
    <UpdateWorkstationContext.Provider
      value={{ modalIsOpen, setModalIsOpen, workstation, setWorkstation }}
    >
      {children}
    </UpdateWorkstationContext.Provider>
  );
}
