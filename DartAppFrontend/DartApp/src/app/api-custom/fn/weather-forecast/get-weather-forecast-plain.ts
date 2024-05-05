/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { WeatherForecast } from '../../models/weather-forecast';

export interface GetWeatherForecast$Plain$Params {
}

export function getWeatherForecast$Plain(http: HttpClient, rootUrl: string, params?: GetWeatherForecast$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<WeatherForecast>>> {
  const rb = new RequestBuilder(rootUrl, getWeatherForecast$Plain.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<WeatherForecast>>;
    })
  );
}

getWeatherForecast$Plain.PATH = '/WeatherForecast';
