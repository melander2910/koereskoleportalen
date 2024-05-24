
import Search from '@/app/ui/search';
import { CreateInvoice } from '@/app/ui/invoices/buttons';
 
export default async function Page() {
  return (
    <div className="w-full">
      <div className="flex w-full items-center justify-between">
        <h1 className={`text-2xl`}>Invoices</h1>
      </div>
      <div className="mt-4 flex items-center justify-between gap-2 md:mt-8">
        <Search placeholder="Search invoices..." />
        <CreateInvoice />
      </div>
      <div className="mt-5 flex w-full justify-center">

      </div>
    </div>
  );
}