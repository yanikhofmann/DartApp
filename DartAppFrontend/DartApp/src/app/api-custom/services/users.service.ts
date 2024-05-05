/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { Get } from '../fn/users/get';
import { Get$Params } from '../fn/users/get';
import { SpGetAllPlayersResult } from '../models/sp-get-all-players-result';

@Injectable({ providedIn: 'root' })
export class UsersService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `getUsers()` */
  static readonly GetUsersPath = '/api/Users';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `Get()` instead.
   *
   * This method doesn't expect any request body.
   */
  Get$Response(params?: Get$Params, context?: HttpContext): Observable<StrictHttpResponse<{
'value': Array<SpGetAllPlayersResult>;
'additionalValues'?: Array<Array<{
}>>;
}>> {
    return Get(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `Get$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  Get(params?: Get$Params, context?: HttpContext): Observable<{
'value': Array<SpGetAllPlayersResult>;
'additionalValues'?: Array<Array<{
}>>;
}> {
    return this.Get$Response(params, context).pipe(
      map((r: StrictHttpResponse<{
'value': Array<SpGetAllPlayersResult>;
'additionalValues'?: Array<Array<{
}>>;
}>): {
'value': Array<SpGetAllPlayersResult>;
'additionalValues'?: Array<Array<{
}>>;
} => r.body)
    );
  }

}
