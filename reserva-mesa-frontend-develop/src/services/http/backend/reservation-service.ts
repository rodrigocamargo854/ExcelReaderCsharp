import HttpClient from "../http-client";
import {
  CreateReservation,
  GetUserReservations,
} from "~/domain/models/reservation";
import { HttpResponse, HttpResponseWithCount } from "../interfaces";
import { CheckIn } from "~/domain/models/check-in";

const createReservation = async (
  reservation: CreateReservation
): Promise<HttpResponse<void>> => {
  let response;

  try {
    response = await HttpClient.post("/reservations", reservation);
  } catch (error) {
    response = error.response;
  }

  return { statusCode: response.statusCode, data: response.data };
};

const getAuthenticatedUserReservationsByDateInterval = async (
  initialDate: string,
  finalDate: string,
  actualPage: number
): Promise<HttpResponse<GetUserReservations>> => {
  let response;
  const url = `/reservations?initialDate=${initialDate}&finalDate=${finalDate}&currentPage=${actualPage}`;

  try {
    response = await HttpClient.get(url);
  } catch (error) {
    response = error.response;
  }

  return {  data: response.data, statusCode: response.statusCode };
};

const getAllActiveReservationsFromToday = async (
  nameFilter: string,
  page: number
): Promise<HttpResponseWithCount<CheckIn>> => {
  let response;
  const url = `/reservations/check-in?currentPage=${page}&nameFilter=${nameFilter}`;

  try {
    response = await HttpClient.get(url);
  } catch (error) {
    response = error.response;
  }

  return {
    data: response.data.data,
    statusCode: response.status,
    count: response.data.count,
  };
};

const checkInReservation = async (
  reservationId: number
): Promise<HttpResponse<void>> => {
  let response;
  const url = `/reservations/check-in?reservationId=${reservationId}`;

  try {
    response = await HttpClient.put(url);
  } catch (error) {
    response = error.response;
  }

  return { statusCode: response.statusCode, data: undefined };
};

const getAllReservedWorkstationsFromDateInterval = async (
  initialDate: string,
  finalDate: string
): Promise<HttpResponse<BlobPart>> => {
  let response;
  const url = `/reservations/reports/reserved-workstations?initialDate=${initialDate}&finalDate=${finalDate}`;

  try {
    response = await HttpClient.post(url);
  } catch (error) {
    response = error.response;
  }

  return { statusCode: response.statusCode, data: response.data };
};

const cancelReservations = async (
  reservationsId: number[]
): Promise<HttpResponse<void>> => {
  let response;

  try {
    response = await HttpClient.put("/reservations/delete", {
      reservationIds: reservationsId,
    });
  } catch (error) {
    response = error.response;
  }

  return { statusCode: response.statusCode, data: undefined };
};

const getAllConfirmedWorkstationsFromDateInterval = async (
  initialDate: string,
  finalDate: string
): Promise<HttpResponse<BlobPart>> => {
  let response;
  const url = `/reservations/reports/confirmed-workstations?initialDate=${initialDate}&finalDate=${finalDate}`;

  try {
    response = await HttpClient.post(url);
  } catch (error) {
    response = error.response;
  }

  return { statusCode: response.statusCode, data: response.data };
};

export {
  createReservation,
  getAuthenticatedUserReservationsByDateInterval,
  cancelReservations,
  getAllReservedWorkstationsFromDateInterval,
  getAllConfirmedWorkstationsFromDateInterval,
  getAllActiveReservationsFromToday,
  checkInReservation,
};
