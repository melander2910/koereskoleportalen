'use client';

import {
  AtSymbolIcon,
  PhoneIcon,
  GlobeEuropeAfricaIcon,
  MapIcon,
  MapPinIcon,
  CurrencyDollarIcon,
} from '@heroicons/react/24/outline';
import { ArrowRightIcon } from '@heroicons/react/20/solid';
import { Button } from './button';
// import { Api as AuthAPI, LoginRequestDto } from '../lib/api/auth-api';
import {
  Api as BackofficeAPI,
  ProductionUnit,
  ProductionUnitUpdateDto,
} from '../lib/api/backoffice-api';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { subtle } from 'crypto';
import { useMutation, useQueryClient } from '@tanstack/react-query';
// import { ProductionUnitUpdateDto } from '../lib/api/backoffice-api';

interface Props {
  subTenant: ProductionUnit | undefined;
}

export default function UpdateProductionUnitForm({ subTenant }: Props) {
  const backOfficeApiClient = new BackofficeAPI({
    baseUrl: 'http://localhost:5199',
    baseApiParams: {
      credentials: 'include',
    },
  }).api;

  const queryClient = useQueryClient();

  const [productionUnit, setProductionUnit] = useState<ProductionUnit>();

  useEffect(() => {
    setProductionUnit({
      name: subTenant?.name,
      phoneNumber: subTenant?.phoneNumber,
      email: subTenant?.email,
      city: subTenant?.city,
      streetAddress: subTenant?.streetAddress,
      zipcode: subTenant?.zipcode,
      price: subTenant?.price,
    });
  }, [subTenant]);

  // updating
  const updateProductionUnitMutation = useMutation({
    mutationFn: (productionUnit: ProductionUnit) =>
      backOfficeApiClient.updateProductionUnit(
        subTenant?.id ?? '',
        productionUnit,
      ),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['subTenant'] });
    },
  });

  // calling update
  function handleUpdate() {
    const updateObject: ProductionUnitUpdateDto = {
      name: productionUnit?.name,
      phoneNumber: productionUnit?.phoneNumber,
      email: productionUnit?.email,
      city: productionUnit?.city,
      streetAddress: productionUnit?.streetAddress,
      zipcode: productionUnit?.zipcode,
      price: productionUnit?.price,
    };
    updateProductionUnitMutation.mutate(updateObject);
  }

  return (
    <div className="space-y-3">
      <div className="flex-1 rounded-lg bg-gray-50 px-6 pb-4 pt-8">
        <div className="w-full">
          <div>
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="name"
            >
              Name
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="name"
                type="name"
                name="name"
                placeholder="Enter your name"
                required
                value={productionUnit?.name ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    name: e.target.value,
                  }));
                }}
              />
              {/* <AtSymbolIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" /> */}
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="phone"
            >
              Phone number
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="phone"
                type="phone"
                name="phone"
                placeholder="Enter phone number"
                required
                value={productionUnit?.phoneNumber ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    phoneNumber: e.target.value,
                  }));
                }}
                // minLength={6}
              />
              <PhoneIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="email"
            >
              Email
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="email"
                type="email"
                name="email"
                placeholder="Enter your email address"
                required
                value={productionUnit?.email ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    email: e.target.value,
                  }));
                }}
                // minLength={6}
              />
              <AtSymbolIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="price"
            >
              Price (DKK)
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="price"
                type="price"
                name="price"
                placeholder="Enter the price"
                required
                value={productionUnit?.price ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    price: Number(e.target.value),
                  }));
                }}
                // minLength={6}
              />
              <CurrencyDollarIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="city"
            >
              City
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="city"
                type="city"
                name="city"
                placeholder="Enter your city"
                required
                value={productionUnit?.city ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    city: e.target.value,
                  }));
                }}
                // minLength={6}
              />
              <MapIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="address"
            >
              Address
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="address"
                type="address"
                name="address"
                placeholder="Enter your address"
                required
                value={productionUnit?.streetAddress ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    streetAddress: e.target.value,
                  }));
                }}
                // minLength={6}
              />
              <MapPinIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="zipcode"
            >
              Zip code
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="zipcode"
                type="zipcode"
                name="zipcode"
                placeholder="Enter your zipcode"
                required
                value={productionUnit?.zipcode ?? ''}
                onChange={(e) => {
                  setProductionUnit((prevState) => ({
                    ...prevState,
                    zipcode: e.target.value,
                  }));
                }}
                // minLength={6}
              />
              <GlobeEuropeAfricaIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
        </div>
        <Button onClick={() => handleUpdate()} className="mt-4 w-full">
          Update <ArrowRightIcon className="ml-auto h-5 w-5 text-gray-50" />
        </Button>
        <div className="flex h-8 items-end space-x-1">
          {/* Add form errors here */}
        </div>
      </div>
    </div>
  );
}
