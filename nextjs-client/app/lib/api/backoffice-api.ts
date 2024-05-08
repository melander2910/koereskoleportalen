/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface Course {
  /** @format uuid */
  id?: string;
  /** @format date-time */
  createdDate?: string;
  /** @format date-time */
  modifiedDate?: string;
  tenantId?: string | null;
  subTenantId?: string | null;
  name?: string | null;
  productionUnit?: ProductionUnit;
  /** @format uuid */
  productionUnitId?: string;
}

export interface Organisation {
  /** @format uuid */
  id?: string;
  /** @format date-time */
  createdDate?: string;
  /** @format date-time */
  modifiedDate?: string;
  cvr?: string | null;
  name?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  claimedByOwner?: boolean | null;
  productionUnits?: ProductionUnit[] | null;
  users?: User[] | null;
  /** @format date-time */
  startDate?: string | null;
  /** @format date-time */
  endDate?: string | null;
  /** @format date-time */
  cvrApiModifiedDate?: string | null;
  industryCode?: string | null;
  industryDescription?: string | null;
  municipality?: string | null;
  country?: string | null;
  city?: string | null;
  streetAddress?: string | null;
  zipcode?: string | null;
  /** @format double */
  latitude?: number | null;
  /** @format double */
  longtitude?: number | null;
  advertisementProtection?: boolean | null;
  organisationType?: string | null;
  status?: string | null;
}

export interface OrganisationCreateDto {
  cvr?: string | null;
  name?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  city?: string | null;
  streetAddress?: string | null;
  zipcode?: string | null;
}

export interface ProductionUnit {
  /** @format uuid */
  id?: string;
  /** @format date-time */
  createdDate?: string;
  /** @format date-time */
  modifiedDate?: string;
  tenantId?: string | null;
  productionUnitNumber?: string | null;
  cvr?: string | null;
  name?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  /** @format date-time */
  startDate?: string | null;
  /** @format date-time */
  endDate?: string | null;
  /** @format date-time */
  cvrApiModifiedDate?: string | null;
  organisation?: Organisation;
  /** @format uuid */
  organisationId?: string;
  users?: User[] | null;
  courses?: Course[] | null;
  municipality?: string | null;
  industryCode?: string | null;
  industryDescription?: string | null;
  country?: string | null;
  city?: string | null;
  streetAddress?: string | null;
  zipcode?: string | null;
  /** @format double */
  latitude?: number | null;
  /** @format double */
  longtitude?: number | null;
  status?: string | null;
}

export interface ProductionUnitCreateDto {
  productionUnitNumber?: string | null;
  cvr?: string | null;
  name?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  city?: string | null;
  streetAddress?: string | null;
  zipcode?: string | null;
}

export interface ProductionUnitUpdateDto {
  name?: string | null;
  phoneNumber?: string | null;
  email?: string | null;
  city?: string | null;
  streetAddress?: string | null;
  zipcode?: string | null;
}

export interface TenantRequestDto {
  tenantId?: string | null;
}

export interface User {
  /** @format uuid */
  id?: string;
  /** @format date-time */
  createdDate?: string;
  /** @format date-time */
  modifiedDate?: string;
  firstname?: string | null;
  lastname?: string | null;
  phoneNumber?: string | null;
  address?: string | null;
  organisations?: Organisation[] | null;
  productionUnits?: ProductionUnit[] | null;
}

export interface UserSignupDto {
  /** @format uuid */
  id?: string;
  firstname?: string | null;
  lastname?: string | null;
  phoneNumber?: string | null;
  address?: string | null;
}

export interface UserUpdateDto {
  firstname?: string | null;
  lastname?: string | null;
  address?: string | null;
}

export type QueryParamsType = Record<string | number, any>;
export type ResponseFormat = keyof Omit<Body, 'body' | 'bodyUsed'>;

export interface FullRequestParams extends Omit<RequestInit, 'body'> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat;
  /** request body */
  body?: unknown;
  /** base url */
  baseUrl?: string;
  /** request cancellation token */
  cancelToken?: CancelToken;
}

export type RequestParams = Omit<
  FullRequestParams,
  'body' | 'method' | 'query' | 'path'
>;

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string;
  baseApiParams?: Omit<RequestParams, 'baseUrl' | 'cancelToken' | 'signal'>;
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<RequestParams | void> | RequestParams | void;
  customFetch?: typeof fetch;
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown>
  extends Response {
  data: D;
  error: E;
}

type CancelToken = Symbol | string | number;

export enum ContentType {
  Json = 'application/json',
  FormData = 'multipart/form-data',
  UrlEncoded = 'application/x-www-form-urlencoded',
  Text = 'text/plain',
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = '';
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>['securityWorker'];
  private abortControllers = new Map<CancelToken, AbortController>();
  private customFetch = (...fetchParams: Parameters<typeof fetch>) =>
    fetch(...fetchParams);

  private baseApiParams: RequestParams = {
    credentials: 'same-origin',
    headers: {},
    redirect: 'follow',
    referrerPolicy: 'no-referrer',
  };

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig);
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key);
    return `${encodedKey}=${encodeURIComponent(
      typeof value === 'number' ? value : `${value}`,
    )}`;
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key]);
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key];
    return value.map((v: any) => this.encodeQueryParam(key, v)).join('&');
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {};
    const keys = Object.keys(query).filter(
      (key) => 'undefined' !== typeof query[key],
    );
    return keys
      .map((key) =>
        Array.isArray(query[key])
          ? this.addArrayQueryParam(query, key)
          : this.addQueryParam(query, key),
      )
      .join('&');
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery);
    return queryString ? `?${queryString}` : '';
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === 'object' || typeof input === 'string')
        ? JSON.stringify(input)
        : input,
    [ContentType.Text]: (input: any) =>
      input !== null && typeof input !== 'string'
        ? JSON.stringify(input)
        : input,
    [ContentType.FormData]: (input: any) =>
      Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key];
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === 'object' && property !== null
            ? JSON.stringify(property)
            : `${property}`,
        );
        return formData;
      }, new FormData()),
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  };

  protected mergeRequestParams(
    params1: RequestParams,
    params2?: RequestParams,
  ): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected createAbortSignal = (
    cancelToken: CancelToken,
  ): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken);
      if (abortController) {
        return abortController.signal;
      }
      return void 0;
    }

    const abortController = new AbortController();
    this.abortControllers.set(cancelToken, abortController);
    return abortController.signal;
  };

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken);

    if (abortController) {
      abortController.abort();
      this.abortControllers.delete(cancelToken);
    }
  };

  public request = async <T = any, E = any>(
    {
      body,
      secure,
      path,
      type,
      query,
      format,
      baseUrl,
      cancelToken,
      ...params
    }: FullRequestParams,
  ): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === 'boolean' ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const queryString = query && this.toQueryString(query);
    const payloadFormatter = this.contentFormatters[type || ContentType.Json];
    const responseFormat = format || requestParams.format;

    return this.customFetch(
      `${baseUrl || this.baseUrl || ''}${path}${
        queryString ? `?${queryString}` : ''
      }`,
      {
        ...requestParams,
        headers: {
          ...(requestParams.headers || {}),
          ...(type && type !== ContentType.FormData
            ? { 'Content-Type': type }
            : {}),
        },
        signal:
          (cancelToken
            ? this.createAbortSignal(cancelToken)
            : requestParams.signal) || null,
        body:
          typeof body === 'undefined' || body === null
            ? null
            : payloadFormatter(body),
      },
    ).then(async (response) => {
      const r = response as HttpResponse<T, E>;
      r.data = null as unknown as T;
      r.error = null as unknown as E;

      const data = !responseFormat
        ? r
        : await response[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data;
              } else {
                r.error = data;
              }
              return r;
            })
            .catch((e) => {
              r.error = e;
              return r;
            });

      if (cancelToken) {
        this.abortControllers.delete(cancelToken);
      }

      if (!response.ok) throw data;
      return data;
    });
  };
}

/**
 * @title BackOffice.API
 * @version 1.0
 */
export class Api<
  SecurityDataType extends unknown,
> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Organisation
     * @name GetOrganisationById
     * @request GET:/api/Organisation/{id}
     * @secure
     */
    getOrganisationById: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Organisation/${id}`,
        method: 'GET',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Organisation
     * @name UpdateOrganisation
     * @request PUT:/api/Organisation/{id}
     * @secure
     */
    updateOrganisation: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Organisation/${id}`,
        method: 'PUT',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Organisation
     * @name DeleteOrganisation
     * @request DELETE:/api/Organisation/{id}
     * @secure
     */
    deleteOrganisation: (id: number, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Organisation/${id}`,
        method: 'DELETE',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Organisation
     * @name GetAllOrganisations
     * @request GET:/api/Organisation
     * @secure
     */
    getAllOrganisations: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Organisation`,
        method: 'GET',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Organisation
     * @name CreateOrganisation
     * @request POST:/api/Organisation
     * @secure
     */
    createOrganisation: (
      data: OrganisationCreateDto,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/Organisation`,
        method: 'POST',
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Organisation
     * @name GetUsersByOrganisationId
     * @request GET:/api/Organisation/{id}/users
     * @secure
     */
    getUsersByOrganisationId: (id: string, params: RequestParams = {}) =>
      this.request<Organisation[], any>({
        path: `/api/Organisation/${id}/users`,
        method: 'GET',
        secure: true,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags ProductionUnit
     * @name GetProductionUnitById
     * @request GET:/api/ProductionUnit/{id}
     * @secure
     */
    getProductionUnitById: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/ProductionUnit/${id}`,
        method: 'GET',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags ProductionUnit
     * @name UpdateProductionUnit
     * @request PUT:/api/ProductionUnit/{id}
     * @secure
     */
    updateProductionUnit: (
      id: string,
      data: ProductionUnitUpdateDto,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/ProductionUnit/${id}`,
        method: 'PUT',
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags ProductionUnit
     * @name DeleteProductionUnit
     * @request DELETE:/api/ProductionUnit/{id}
     * @secure
     */
    deleteProductionUnit: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/ProductionUnit/${id}`,
        method: 'DELETE',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags ProductionUnit
     * @name GetAllProductionUnits
     * @request GET:/api/ProductionUnit
     * @secure
     */
    getAllProductionUnits: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/ProductionUnit`,
        method: 'GET',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags ProductionUnit
     * @name CreateProductionUnit
     * @request POST:/api/ProductionUnit
     * @secure
     */
    createProductionUnit: (
      data: ProductionUnitCreateDto,
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/ProductionUnit`,
        method: 'POST',
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags ProductionUnit
     * @name GetUsersByProductionUnitId
     * @request GET:/api/ProductionUnit/{id}/users
     * @secure
     */
    getUsersByProductionUnitId: (id: string, params: RequestParams = {}) =>
      this.request<ProductionUnit[], any>({
        path: `/api/ProductionUnit/${id}/users`,
        method: 'GET',
        secure: true,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Tenant
     * @name GetTenantsByUserId
     * @request GET:/api/Tenant
     * @secure
     */
    getTenantsByUserId: (params: RequestParams = {}) =>
      this.request<Organisation[], any>({
        path: `/api/Tenant`,
        method: 'GET',
        secure: true,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags Tenant
     * @name SetTenantId
     * @request POST:/api/Tenant
     * @secure
     */
    setTenantId: (data: TenantRequestDto, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/Tenant`,
        method: 'POST',
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name GetUserById
     * @request GET:/api/User/{id}
     * @secure
     */
    getUserById: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/User/${id}`,
        method: 'GET',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name UpdateUser
     * @request PUT:/api/User/{id}
     * @secure
     */
    updateUser: (id: string, data: UserUpdateDto, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/User/${id}`,
        method: 'PUT',
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name DeleteUser
     * @request DELETE:/api/User/{id}
     * @secure
     */
    deleteUser: (id: string, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/User/${id}`,
        method: 'DELETE',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name GetAllUsers
     * @request GET:/api/User
     * @secure
     */
    getAllUsers: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/User`,
        method: 'GET',
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name CreateUser
     * @request POST:/api/User
     * @secure
     */
    createUser: (data: UserSignupDto, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/User`,
        method: 'POST',
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name GetOrganisationsByUserId
     * @request GET:/api/User/{id}/organisations
     * @secure
     */
    getOrganisationsByUserId: (id: string, params: RequestParams = {}) =>
      this.request<Organisation[], any>({
        path: `/api/User/${id}/organisations`,
        method: 'GET',
        secure: true,
        format: 'json',
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name GetProductionUnitsByUserId
     * @request GET:/api/User/{id}/productionunits
     * @secure
     */
    getProductionUnitsByUserId: (id: string, params: RequestParams = {}) =>
      this.request<ProductionUnit[], any>({
        path: `/api/User/${id}/productionunits`,
        method: 'GET',
        secure: true,
        format: 'json',
        ...params,
      }),
  };
}
