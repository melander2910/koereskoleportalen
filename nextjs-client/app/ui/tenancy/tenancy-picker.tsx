'use client';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { PowerIcon } from '@heroicons/react/24/outline';
import { Api } from '@/app/lib/api/backoffice-api';
import { useQuery } from '@tanstack/react-query';
import clsx from 'clsx';


export default function TenancyPicker({
  params,
}: {
  params: { tenant: string };
}) {
  const backOfficeClient = new Api({
    baseUrl: 'http://localhost:5199',
    baseApiParams: {
      credentials: 'include',
    },
  }).api;

  const pathname = usePathname();
  console.log(pathname);
  const {
    status: userTenantsStatus,
    error: userTenantsError,
    data: userTenantsData,
  } = useQuery({
    queryKey: ['userTenants'],
    queryFn: async () => {
      const response = await backOfficeClient.getTenantsByUserId();
      return response.data; // Extract the data from the response
    },
  });

  return (
    <div className="flex h-full flex-col px-3 py-4 md:px-2">
      <Link
        className="mb-2 flex h-20 items-end justify-start rounded-md bg-blue-600 p-4 md:h-40"
        href="/"
      ></Link>
      <div className="flex grow flex-row justify-between space-x-2 md:flex-col md:space-x-0 md:space-y-2">
        {userTenantsData?.map((tenant: any) => {
          return (
            <Link
              key={tenant.cvr}
              href={`/${tenant.cvr}`}
              className={clsx(
                'flex h-[48px] grow items-center justify-center gap-2 rounded-md bg-gray-50 p-3 text-sm font-medium hover:bg-sky-100 hover:text-blue-600 md:flex-none md:justify-start md:p-2 md:px-3',
                {
                  'bg-sky-100 text-blue-600': pathname.substring(1,) === tenant.cvr,
                },
              )}
            >
              <p className="hidden md:block">{tenant.name}</p>
            </Link>
          );
        })}
        <div className="hidden h-auto w-full grow rounded-md bg-gray-50 md:block"></div>
        <form>
          <button className="flex h-[48px] w-full grow items-center justify-center gap-2 rounded-md bg-gray-50 p-3 text-sm font-medium hover:bg-sky-100 hover:text-blue-600 md:flex-none md:justify-start md:p-2 md:px-3">
            <PowerIcon className="w-6" />
            <div className="hidden md:block">Sign Out</div>
          </button>
        </form>
      </div>
    </div>
  );
}
