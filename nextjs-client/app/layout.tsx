'use client'
import '@/app/ui/global.css';
import { roboto } from './ui/fonts';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const queryClient = new QueryClient();

  return (
    <html lang="en">
      <body className={`${roboto.className} antialiased`}>
        <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
        </body>
    </html>
  );
}
