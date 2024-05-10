'use client';
import { Card } from '@/app/ui/dashboard/cards';
import RevenueChart from '@/app/ui/dashboard/revenue-chart';
import LatestInvoices from '@/app/ui/dashboard/latest-invoices';
import { customers, invoices, revenue } from '../../../../lib/placeholder-data';
import { Suspense, useEffect, useState } from 'react';
import { Customer, LatestInvoice } from '../../../../lib/definitions';
import { Api, ProductionUnit } from '../../../../lib/api/backoffice-api';
import { RevenueChartSkeleton } from '@/app/ui/skeletons';
import UpdateProductionUnitForm from '@/app/ui/update-productionunit-form';
import { useQuery } from '@tanstack/react-query';

export default function Page({
  params,
}: {
  params: { tenant: string; subtenant: string };
}) {
  const backOfficeClient = new Api({
    baseUrl: 'http://localhost:5199',
    baseApiParams: {
      credentials: 'include',
    },
  }).api;

  const {
    status: subTenantStatus,
    error: subTenantError,
    data: subTenantData,
  } = useQuery({
    queryKey: ['subTenant' + params.subtenant],
    queryFn: async () => {
      const response =
        await backOfficeClient.getProductionUnitByProductionUnitNumber(
          params.subtenant,
        );
      return response.data; // Extract the data from the response
    },
  });

  return (
    <main>
      <h1 className="mb-4 text-xl md:text-2xl">Dashboard</h1>
      <div>Tenant id: {params.tenant}</div>
      <div>Sub Tenant id: {params.subtenant}</div>

      <UpdateProductionUnitForm subTenant={subTenantData} />
    </main>
  );
}
