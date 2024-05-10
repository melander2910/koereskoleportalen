'use client';

import {
  AtSymbolIcon,
  KeyIcon,
  ExclamationCircleIcon,
} from '@heroicons/react/24/outline';
import { ArrowRightIcon } from '@heroicons/react/20/solid';
import { Button } from './button';
import { Api as AuthAPI, LoginRequestDto } from '../lib/api/auth-api';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

export default function LoginForm() {
  const authClient = new AuthAPI({
    baseUrl: 'http://localhost:5167',
    baseApiParams: {
      credentials: 'include',
    },
  }).api;

  const router = useRouter();
  const [loginRequestDto, setLoginRequestDto] = useState<LoginRequestDto>({});

  const handleLogin = async () => {
    console.log('Login');
    var response = await authClient.login(loginRequestDto);
    var data = await response.json();
    console.log(data.result);
    router.push(`/${data.result.tenantClaims[0]}`);
  };

  return (
    <div className="space-y-3">
      <div className="flex-1 rounded-lg bg-gray-50 px-6 pb-4 pt-8">
        <h1 className={`mb-3 text-2xl`}>Please log in to continue.</h1>
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
                value={loginRequestDto.username ?? ''}
                onChange={(e) => {
                  setLoginRequestDto((prevState) => ({
                    ...prevState,
                    username: e.target.value,
                  }));
                }}
              />
              <AtSymbolIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
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
                  setLoginRequestDto((prevState) => ({
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
        <Button onClick={() => handleLogin()} className="mt-4 w-full">
          Log in <ArrowRightIcon className="ml-auto h-5 w-5 text-gray-50" />
        </Button>
        <div>
          <h2>
            Dont have an account?{' '}
            <Link className="text-sky-500" href="/register">
              Click here
            </Link>
          </h2>
        </div>
        <div className="flex h-8 items-end space-x-1">
          {/* Add form errors here */}
        </div>
      </div>
    </div>
  );
}
