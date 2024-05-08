import { Card } from '@/app/ui/dashboard/cards';
import RevenueChart from '@/app/ui/dashboard/revenue-chart';
import LatestInvoices from '@/app/ui/dashboard/latest-invoices';
import { customers, invoices, revenue } from '../../../../lib/placeholder-data';
import { Suspense } from 'react';
import { Customer, LatestInvoice } from '../../../../lib/definitions'
import { Api } from '../../../../lib/api/backoffice-api';
import { RevenueChartSkeleton } from '@/app/ui/skeletons';
 
export default async function Page({ params }: { params: { tenant: string, subtenant: string } }) {

//   const backOfficeClient = new Api({
//     baseUrl: "http://localhost:5199",
//     // baseApiParams: {
//     //     credentials: "include",
//     // }
//   }).api;
// // https://localhost:7170
//   const getUserTenants = async () => {
//     var response = await backOfficeClient.getTenantsByUserId();
//     var data = await response.json();
//     return data
//   }

  // var userTenants = await getUserTenants();
  // console.log(userTenants);
  const latestInvoices: LatestInvoice[] = [];

  return (
    <main>
      <h1 className="mb-4 text-xl md:text-2xl">
        Dashboard
      </h1>
      <div>Tenant id: {params.tenant}</div>
      <div>Sub Tenant id: {params.subtenant}</div>
      <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
        <Card title="Collected" value={13} type="collected" />
        <Card title="Pending" value={19} type="pending" />
        <Card title="Total Invoices" value={32} type="invoices" />
        <Card
          title="Total Tenants"
          value={12}
          // value={userTenants.length}
          type="customers"
        />
      </div>
      <div className="mt-6 grid grid-cols-1 gap-6 md:grid-cols-4 lg:grid-cols-8">
        <Suspense fallback={<RevenueChartSkeleton/>}>
          <RevenueChart />
        </Suspense>
        <LatestInvoices latestInvoices={latestInvoices} />
      </div>
    </main>
  );
}