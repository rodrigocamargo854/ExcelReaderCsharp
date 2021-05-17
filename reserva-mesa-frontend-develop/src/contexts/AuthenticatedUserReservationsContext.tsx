import moment from 'moment';
import React, { createContext, ReactNode, useEffect, useState } from 'react';

import { GetUserReservations, Reservation } from "~/domain/models/reservation";
import { HttpResponse } from '~/services/http/interfaces';
import {
  getAuthenticatedUserReservationsByDateInterval
} from '~/services/http/backend/reservation-service';
import { PopupStatus, showPopup } from '~/application/popup';

interface AuthenticatedUserReservationsContextData {
  quantityPerPage: number;
  reservations: Reservation[];
  initialDate: string;
  finalDate?: string;
  actualPage: number;
  count: number;
  selectedReservationsId: number[];
  setReservations: (value: Reservation[]) => void;
  setInitialDate: (value: string) => void;
  setFinalDate: (value: string) => void;
  setActualPage: (value: number) => void;
  setCount: (value: number) => void;
  setSelectedReservationsId: (value: number[]) => void;
  fetchReservations: () => Promise<void>;
}

interface AuthenticatedUserReservationsProviderProps {
  children: ReactNode;
}

export const AuthenticatedUserReservationsContext = createContext<AuthenticatedUserReservationsContextData>({} as AuthenticatedUserReservationsContextData);

export function AuthenticatedUserReservationsProvider({ children }: AuthenticatedUserReservationsProviderProps) {
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [actualPage, setActualPage] = useState<number>(0);
  const [initialDate, setInitialDate] = useState<string>('');
  const [finalDate, setFinalDate] = useState<string>();
  const [count, setCount] = useState<number>(0);
  const [selectedReservationsId, setSelectedReservationsId] = useState<number[]>([]);

  const quantityPerPage = 14;

  function validateDates(): boolean {
    const finalDateFormValue = moment(finalDate);
    const initialDateFormValue = moment(initialDate);
    return finalDateFormValue.isAfter(initialDateFormValue, 'day');
  }

  async function fetchReservations() {
    if (finalDate && initialDate) {
      if (validateDates()) {
        const response =
          await getAuthenticatedUserReservationsByDateInterval(initialDate, finalDate, actualPage);
        treatResponse(response);
      } else {
        showPopup({
          title: "Erro de validação",
          message: "A data final não pode ser uma data anterior à data inicial.",
          status: PopupStatus.Error
        })
      }
    }
  }

  function removeDuplicates(array: Array<any>): Array<any> {
    return [...new Set(array)];
  }

  function treatResponse(response: HttpResponse<GetUserReservations>) {
    setCount(response.data.count);

    if (response.data.data.length) {
      if (actualPage !== 0) {
        setReservations(removeDuplicates([ ...reservations, ...response.data.data ]));
      } else {
        setReservations(response.data.data);
      }
    } else {
      if (actualPage === 0) {
        setReservations([]);
      }
    }
  }

  useEffect(() => {
    fetchReservations();
  }, [actualPage]);

  return (
    <AuthenticatedUserReservationsContext.Provider
    value={{
      fetchReservations,
      quantityPerPage,
      reservations,
      count,
      initialDate,
      finalDate,
      actualPage,
      selectedReservationsId,
      setCount,
      setInitialDate,
      setFinalDate,
      setReservations,
      setActualPage,
      setSelectedReservationsId
    }}>
      { children }
    </AuthenticatedUserReservationsContext.Provider>
  );
}
