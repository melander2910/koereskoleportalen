export default function Page({ params }: { params: { tenant: string, subtenant: string } }) {
    return (
        <>
        <div>My tenant: {params.tenant}</div>
        <div>My subtenant: {params.subtenant}</div>
        </>
    );
  }
  