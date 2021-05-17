import { Unit } from "~/domain/models/unit";
import { HttpResponse } from "../interfaces";
import HttpClient from "../http-client";

const getUnits = async (): Promise<HttpResponse<Array<Unit>>> => {
  let response;

  try {
    response = await HttpClient.get("/units");
  } catch (error) {
    response = error.response;
  }

  return { data: response.data, statusCode: response.statusCode };
};

export { getUnits };
