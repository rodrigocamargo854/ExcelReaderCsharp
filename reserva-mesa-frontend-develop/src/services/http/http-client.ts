import axios, { AxiosInstance, AxiosResponse } from "axios";
import { getToken } from "../auth/adal";
import { HttpResponse } from "./interfaces";
import history from "~/application/routes/history";
import backendConfig from "~/config/backendConfig";
class HttpClient {
  private readonly _axios: AxiosInstance;

  constructor () {
    this._axios = axios.create({
      baseURL: backendConfig.backend_url,
      headers: {
        Authorization: `Bearer ${getToken()}`,
      }
    });
  }

  private convertAxiosResponseToHttpResponse(response: AxiosResponse<any>): HttpResponse<any> {
    return { statusCode: response.status, data: response.data };
  }

  private treatUnexpectedResponse(response: AxiosResponse<any>) {
    if(response.status === 500){
      history.push("/error-500");
    }

    return this.convertAxiosResponseToHttpResponse(response);
  }

  public async post(url: string, body: any = {}): Promise<HttpResponse<any>> {
    let response;

    try {
      response = await this._axios.post(url, body);
      response = this.convertAxiosResponseToHttpResponse(response);
    } catch (error) {
      response = this.treatUnexpectedResponse(error.response);
    }

    return { statusCode: response.statusCode, data: response.data };
  }

  public async put(url: string, body: any = {}): Promise<HttpResponse<any>> {
    let response;

    try {
      response = await this._axios.put(url, body);
      response = this.convertAxiosResponseToHttpResponse(response);
    } catch (error) {
      response = this.treatUnexpectedResponse(error.response);
    }

    return { statusCode: response.statusCode, data: response.data };
  }

  public async get(url: string): Promise<HttpResponse<any>> {
    let response;

    try {
      response = await this._axios.get(url);
      response = this.convertAxiosResponseToHttpResponse(response);
    } catch (error) {
      response = this.treatUnexpectedResponse(error.response);
    }

    return { statusCode: response.statusCode, data: response.data };
  }

  public async delete(url: string): Promise<HttpResponse<any>> {
    let response;

    try {
      response = await this._axios.delete(url);
      response = this.convertAxiosResponseToHttpResponse(response);
    } catch (error) {
      response = this.treatUnexpectedResponse(error.response);
    }

    return { statusCode: response.statusCode, data: response.data };
  }
}

export default new HttpClient();
