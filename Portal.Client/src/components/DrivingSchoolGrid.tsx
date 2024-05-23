import useDrivingSchools from "@/hooks/useDrivingSchools";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "./ui/card";

interface Props {
    searchInput: string;
}

function DrivingSchoolGrid({ searchInput }: Props) {
    const { data: drivingschools } = useDrivingSchools();

    return (
        <div className="grid grid-cols-4 gap-4">
            {drivingschools
                .filter((drivingSchool) => drivingSchool.name?.toLowerCase().includes(searchInput.toLowerCase()))
                .map((drivingSchool) => (
                    <Card key={drivingSchool.id} className="flex flex-col justify-between h-64">
                        <CardHeader className="flex-grow-0">
                            <CardDescription>{drivingSchool.city}</CardDescription>
                            <CardTitle className="min-h-12">{drivingSchool.name}</CardTitle>
                        </CardHeader>
                        <CardContent className="flex-grow">
                            <div>
                                {drivingSchool.address}, {drivingSchool.zip}
                            </div>
                        </CardContent>
                        <CardFooter className="justify-end">{drivingSchool.price > 0 ? `${drivingSchool.price},-` : "Pris utilgængelig"}</CardFooter>
                    </Card>
                ))}
        </div>
    );
}

export default DrivingSchoolGrid;
