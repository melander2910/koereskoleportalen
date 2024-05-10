'use client';

import {
  AtSymbolIcon,
  KeyIcon,
  PhoneIcon,
  UserIcon,
} from '@heroicons/react/24/outline';
import { ArrowRightIcon } from '@heroicons/react/20/solid';
import { Button } from './button';
import { Api as AuthAPI, RegistrationRequestDto } from '../lib/api/auth-api';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function RegisterForm() {
  const authClient = new AuthAPI({
    baseUrl: 'http://localhost:5167',
    baseApiParams: {
      credentials: 'include',
    },
  }).api;

  const router = useRouter();
  const [registrationRequestDto, setRegistrationRequestDto] =
    useState<RegistrationRequestDto>({});

  const handleRegister = async () => {
    var response = await authClient.signup(registrationRequestDto);
    var data = await response.json();

    router.push('/login');
  };

  return (
    <div className="space-y-3">
      <div className="flex-1 rounded-lg bg-gray-50 px-6 pb-4 pt-8">
        <h1 className={`mb-3 text-2xl`}>Register account.</h1>
        <div className="w-full">
          <div>
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
                value={registrationRequestDto.email ?? ''}
                onChange={(e) => {
                  setRegistrationRequestDto((prevState) => ({
                    ...prevState,
                    email: e.target.value,
                  }));
                }}
              />
              <AtSymbolIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
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
                placeholder="Enter you full name"
                required
                value={registrationRequestDto.name ?? ''}
                onChange={(e) => {
                  setRegistrationRequestDto((prevState) => ({
                    ...prevState,
                    name: e.target.value,
                  }));
                }}
              />
              <UserIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div>
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="phone"
            >
              Phone
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="phone"
                type="phone"
                name="phone"
                placeholder="Enter your phone number"
                required
                value={registrationRequestDto.phoneNumber ?? ''}
                onChange={(e) => {
                  setRegistrationRequestDto((prevState) => ({
                    ...prevState,
                    phoneNumber: e.target.value,
                  }));
                }}
              />
              <PhoneIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-gray-900"
              htmlFor="password"
            >
              Password
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-md border border-gray-200 py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500"
                id="password"
                type="password"
                name="password"
                placeholder="Enter password"
                required
                onChange={(e) => {
                  setRegistrationRequestDto((prevState) => ({
                    ...prevState,
                    password: e.target.value,
                  }));
                }}
                // minLength={6}
              />
              <KeyIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
          </div>
        </div>
        <Button onClick={() => handleRegister()} className="mt-4 w-full">
          Register <ArrowRightIcon className="ml-auto h-5 w-5 text-gray-50" />
        </Button>
        <h2>
          Back to login{' '}
          <Link className="text-sky-500" href="/login">
            Click here
          </Link>
        </h2>
        <div className="flex h-8 items-end space-x-1">
          {/* Add form errors here */}
        </div>
      </div>
    </div>
  );
}
