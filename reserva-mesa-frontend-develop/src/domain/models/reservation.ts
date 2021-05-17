interface CreateReservation {
  workstationName: string;
  initialDate: Date;
  finalDate: Date;
}

interface Reservation {
  id?: number;
  workstationName: string;
  date: Date | string;
}

interface GetUserReservations {
  count: number;
  data: Array<Reservation>
}

export type { CreateReservation, Reservation, GetUserReservations };
