import { DrivingSchool } from "@/@types/DrivingSchool";
import useData from "./useData";

const useDrivingSchools = () => {
    return useData<DrivingSchool>("api/DrivingSchool");
};

export default useDrivingSchools;
