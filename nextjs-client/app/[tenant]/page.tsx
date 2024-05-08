import { Card } from "../ui/dashboard/cards";
import SideNav from "../ui/dashboard/sidenav";
import TenancyPicker from "../ui/tenancy/tenancy-picker";

export default function Page({ params }: { params: { tenant: string } }) {
  return (

    

    <main>
      <div>My tenant PAGE: {params.tenant}</div>
      <div>Get production units by CVR</div>
      <div className="rounded-xl bg-gray-50 p-2 shadow-sm">
        <div className="flex p-4">
          {/* {Icon ? <Icon className="h-5 w-5 text-gray-700" /> : null} */}
          <h3 className="ml-2 text-sm font-medium">{params.tenant}</h3>
        </div>
        <p className="truncate rounded-xl bg-white px-4 py-8 text-center text-2xl">
          {params.tenant}
        </p>
      </div>

      <h1 className="mb-4 text-xl md:text-2xl">Dashboard</h1>
      <div>Tenant id: {params.tenant}</div>
      <div>Sub Tenant id:</div>
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
    </main>
  );
}
