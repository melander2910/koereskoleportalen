import SideNav from '@/app/ui/dashboard/sidenav';
 
export default function Layout({ children, params }: { children: React.ReactNode, params: { tenant: string, subtenant: string } }) {
  return (
    <div className="flex h-screen flex-col md:flex-row md:overflow-hidden">
      <div className="w-full flex-none md:w-64">
        <SideNav params={{tenant: params.tenant, subtenant: params.subtenant }}/>
      </div>
      <div className="flex-grow p-6 md:overflow-y-auto md:p-12">{children}</div>
    </div>
  );
}