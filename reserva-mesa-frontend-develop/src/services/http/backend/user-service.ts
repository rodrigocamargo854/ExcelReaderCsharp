import { UserRoles } from "~/domain/models/user";
import HttpClient from "../http-client";
import { HttpResponse } from "../interfaces";

const getUserRoles = async (): Promise<HttpResponse<UserRoles>> => {
  let response;

  try {
    response = await HttpClient.get("/users/roles");
  } catch (error) {
    response = error.response;
  }

  return response;
}

export { getUserRoles };
