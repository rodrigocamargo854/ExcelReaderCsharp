import { Floor, UpdateFloor } from "~/domain/models/floor";
import { HttpResponse } from "../interfaces";
import HttpClient from "../http-client";

const getFloorsByUnitId = async (
  unitId: number
): Promise<HttpResponse<Array<Floor>>> => {
  let response;

  try {
    response = await HttpClient.get(`/floors/units/${unitId}`);
  } catch (error) {
    response = error.response;
  }

  return { data: response.data, statusCode: response.statusCode };
};

const updateFloor = async (
  floorId: number,
  requestModel: UpdateFloor
): Promise<HttpResponse<void>> => {
  let response;

  try {
    response = await HttpClient.put(`/floors/${floorId}`, requestModel);
  } catch (error) {
    response = error.response;
  }

  return { data: response.data, statusCode: response.statusCode };
};

const getFloorByCode = async (code: string): Promise<HttpResponse<Floor>> => {
  let response;

  try {
    response = await HttpClient.get(`/floors?code=${code}`);
  } catch (error) {
    response = error.response;
  }

  return { data: response.data, statusCode: response.statusCode };
};

const getFloorById = async (id: number): Promise<HttpResponse<Floor>> => {
  let response;

  try {
    response = await HttpClient.get(`/floors/${id}`);
  } catch (error) {
    response = error.response;
  }

  return { data: response.data, statusCode: response.statusCode };
};

export { getFloorsByUnitId, updateFloor, getFloorByCode, getFloorById };
