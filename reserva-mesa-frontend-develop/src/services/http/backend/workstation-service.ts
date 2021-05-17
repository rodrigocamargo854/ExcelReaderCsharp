import HttpClient from "../http-client";
import { HttpResponse } from "../interfaces";
import { Workstation } from "~/domain/models/workstation";

const getDisabledWorkstations = async (
  floorId: number
): Promise<HttpResponse<Workstation[]>> => {
  let response;
  try {
    response = await HttpClient.get(
      `/workstations/inactives?floorId=${floorId}`
    );
  } catch (error) {
    response = error.response;
  }
  return { statusCode: response.statusCode, data: response.data };
};

const getReservedWorkstationByFloorIdAndDateInterval = async (
  floorId: number,
  initialDate: string,
  finalDate: string
): Promise<HttpResponse<string[]>> => {
  let response;
  try {
    response = await HttpClient.get(
      `/reservations/workstations?floorId=${floorId}&initialDate=${initialDate}&finalDate=${finalDate}`
    );
  } catch (error) {
    response = error.response;
  }
  return { statusCode: response.statusCode, data: response.data };
};

const getWorkstationByName = async (
  name: string
): Promise<HttpResponse<Workstation>> => {
  let response;
  try {
    response = await HttpClient.get(`/workstations/${name}`);
  } catch (error) {
    response = error.response;
  }
  return { statusCode: response.statusCode, data: response.data };
};

const updateWorkstation = async (
  workstation: Workstation
): Promise<HttpResponse<void>> => {
  let response;
  try {
    response = await HttpClient.put("/workstations", workstation);
  } catch (error) {
    response = error.response;
  }
  return { statusCode: response.statusCode, data: response.data };
};

export {
  getDisabledWorkstations,
  getReservedWorkstationByFloorIdAndDateInterval,
  getWorkstationByName,
  updateWorkstation,
};
