/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { WeatherForecast } from '../../models/weather-forecast';

export interface GetWeatherForecast$Json$Params {
}

export function getWeatherForecast$Json(http: HttpClient, rootUrl: string, params?: GetWeatherForecast$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<WeatherForecast>>> {
  const rb = new RequestBuilder(rootUrl, getWeatherForecast$Json.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<WeatherForecast>>;
    })
  );
}

getWeatherForecast$Json.PATH = '/WeatherForecast';
