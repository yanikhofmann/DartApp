/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { SpGetAllPlayersResult } from '../../models/sp-get-all-players-result';

export interface Get$Params {
}

export function Get(http: HttpClient, rootUrl: string, params?: Get$Params, context?: HttpContext): Observable<StrictHttpResponse<{
'value': Array<SpGetAllPlayersResult>;
'additionalValues'?: Array<Array<{
}>>;
}>> {
  const rb = new RequestBuilder(rootUrl, Get.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'application/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<{
      'value': Array<SpGetAllPlayersResult>;
      'additionalValues'?: Array<Array<{
      }>>;
      }>;
    })
  );
}

Get.PATH = '/api/Users';
