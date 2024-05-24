'use client';
import Link from 'next/link';
import NavLinks from '@/app/ui/dashboard/nav-links';
import { PowerIcon } from '@heroicons/react/24/outline';
import { useRouter } from 'next/navigation'


export default function SideNav({ params }: { params: { tenant: string, subtenant: string } }) {
  const router = useRouter();
  const handleLogout = () => {
  router.push('/logout');
};
return (
    <div className="flex h-full flex-col px-3 py-4 md:px-2">
      <Link
        className="mb-2 flex h-20 items-end justify-start rounded-md bg-blue-200 p-4 md:h-10"
        href={`/${params.tenant}`}
      >
      </Link>
      <div className="flex grow flex-row justify-between space-x-2 md:flex-col md:space-x-0 md:space-y-2">
        <NavLinks params={{tenant: params.tenant, subtenant: params.subtenant }}/>
        <div className="hidden h-auto w-full grow rounded-md bg-gray-50 md:block"></div>
          <button onClick={()=> handleLogout()} className="flex h-[48px] w-full grow items-center justify-center gap-2 rounded-md bg-gray-50 p-3 text-sm font-medium hover:bg-sky-100 hover:text-blue-600 md:flex-none md:justify-start md:p-2 md:px-3">
            <PowerIcon className="w-6" />
            <div className="hidden md:block">Sign Out</div>
          </button>
      </div>
    </div>
  );
}
