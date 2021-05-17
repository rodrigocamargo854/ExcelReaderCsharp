export interface HttpResponse<T> {
  data: T;
  statusCode: number;
}

export interface HttpResponseWithCount<T> {
  data: T[];
  statusCode: number;
  count: number;
}
