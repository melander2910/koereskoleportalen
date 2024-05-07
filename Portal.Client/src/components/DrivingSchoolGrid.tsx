import useDrivingSchools from "@/hooks/useDrivingSchools";
import { Card, CardContent, CardHeader, CardTitle } from "./ui/card";

interface Props {
    searchInput: string;
}

function DrivingSchoolGrid({ searchInput }: Props) {
    const { data: drivingschools } = useDrivingSchools();

    return (
        <>
            <div className="grid grid-cols-4 gap-4">
                {drivingschools
                    .filter((drivingSchool) => drivingSchool.name?.toLowerCase().includes(searchInput.toLowerCase()))
                    .map((drivingSchool) => (
                        <Card key={drivingSchool.id}>
                            <CardHeader>
                                <CardTitle>{drivingSchool.name}</CardTitle>
                            </CardHeader>
                            <CardContent>
                                <p>
                                    {drivingSchool.address}, {drivingSchool.zip}, {drivingSchool.city}
                                </p>
                            </CardContent>
                        </Card>
                    ))}
            </div>
        </>
    );
}

export default DrivingSchoolGrid;
