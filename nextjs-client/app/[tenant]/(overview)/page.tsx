'use client';
import { usePathname } from 'next/navigation';
import { Api } from '../../lib/api/backoffice-api';
import SubTenancyPicker from '../../ui/tenancy/sub-tenancy-picker';

export default function Page({ params }: { params: { tenant: string } }) {
  const backOfficeClient = new Api({
    baseUrl: 'http://localhost:5199',
    baseApiParams: {
      credentials: 'include',
    },
  }).api;

  const pathname = usePathname();

  return (
    <main>
      <SubTenancyPicker
        params={{
          tenant: params.tenant,
        }}
      ></SubTenancyPicker>
    </main>
  );
}
