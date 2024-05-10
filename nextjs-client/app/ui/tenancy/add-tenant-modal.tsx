'use client'
import React, { useState } from 'react';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/app/ui/dialog';
import { PlusIcon } from '@radix-ui/react-icons';
import { Label } from '@/app/ui/label';
import { Input } from '@/app/ui/input';
import { Button } from '../button';
import { Api as AuthAPI, CreateClaimDto, LoginRequestDto } from '../../lib/api/auth-api';


interface Props {
  claimType: "tenant" | "subtenant"
  displayText: string
}


export default function AddTenantModal({claimType, displayText}: Props) {
  const [tempTenant, setTempTenant] = useState<CreateClaimDto>({claimType: claimType, claimValue: ""})
  
  const authClient = new AuthAPI({
    baseUrl: 'http://localhost:5167',
    baseApiParams: {
      credentials: 'include',
    },
  }).api; 

  const handleAddTentantClaim = async () => {
    console.log(tempTenant);
    if(tempTenant.claimValue != ""){
      var response = await authClient.authClaimCreate(tempTenant)
      var data = await response.json();
    }
  }

  return (
    <Dialog>
      <DialogTrigger asChild>
        <button className="flex h-[48px] w-full grow items-center justify-center gap-2 rounded-md bg-gray-50 p-3 text-sm font-medium hover:bg-sky-100 hover:text-blue-600 md:flex-none md:justify-start md:p-2 md:px-3">
            <PlusIcon className="w-6" />
            <p className="hidden md:block">New {displayText}</p>
        </button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Claim {displayText}</DialogTitle>
          <DialogDescription>
            To claim your {displayText}, you must be the organisation owner, provide the valid {claimType == 'tenant' ? 'CVR number' : 'Production Unit Number'} and go through our manual verification flow.
          </DialogDescription>
        </DialogHeader>
        <div className="grid gap-4 py-4">
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="name" className="text-left">
              {claimType == "tenant" ? 'CVR' : 'Production Unit Number'}
            </Label>
            <Input
              id="name"
              defaultValue=""
              className="col-span-3"
              onChange={(e) => {
                setTempTenant((prevState) => ({
                  ...prevState,
                  claimValue: e.target.value,
                }));
              }}
            />
          </div>
        </div>
        <DialogFooter className="sm:justify-end">
          <Button onClick={() => {handleAddTentantClaim()}} type="submit">Claim {displayText}</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
