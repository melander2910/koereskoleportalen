'use client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { Api, ProductionUnit } from '@/app/lib/api/backoffice-api';
import { useQuery } from '@tanstack/react-query';
import clsx from 'clsx';
import { PlusIcon, PowerIcon } from '@heroicons/react/24/outline';
import AddTenantModal from './add-tenant-modal';

export default function SubTenancyPicker({
  params,
}: {
  params: { tenant: string };
}) {
  const pathname = usePathname();
  const backOfficeClient = new Api({
    baseUrl: 'http://localhost:5199',
    baseApiParams: {
      credentials: 'include',
      headers: {
        tenant: `${params.tenant}`,
      },
    },
  }).api;

  const {
    status: userSubTenantsStatus,
    error: userSubTenantsError,
    data: userSubTenantsData,
  } = useQuery({
    // querykey should have userId, and tenant?
    queryKey: ['userSubTenants' + params.tenant],
    queryFn: async () => {
      const response = await backOfficeClient.getSubTenantsByUserId();
      return response.data; // Extract the data from the response
    },
  });

  const handleAddSubTenantClaim = () => {

  }

  return (
    <>
    <div className='mb-5'>
    <AddTenantModal claimType='subtenant' displayText='production unit'/>
    </div>
    <div className="grid gap-4 md:grid-cols-2 sm:grid-cols-4 lg:grid-cols-4">
      {userSubTenantsData?.map((subtenant) => {
        return (
            <Link
              key={subtenant.productionUnitNumber}
              href={`${pathname}/${subtenant.productionUnitNumber}/home`}
              className="items-center justify-center gap-2 rounded-md bg-gray-50 p-3 text-sm font-medium hover:bg-sky-100 hover:text-blue-600 md:flex-none md:justify-start md:p-2 md:px-3"
            >

                <div className="p-4">
                  <h3 className="ml-2 text-sm font-medium">
                    {subtenant.name} {subtenant.city}
                  </h3>
                </div>


                <p className="truncate rounded-xl bg-white px-4 py-8 text-center text-2xl">
                  {subtenant.productionUnitNumber}
                </p>
            </Link>
        );
      })}
      
    </div>
    
</>
  );
}
