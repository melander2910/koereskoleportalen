import { useState } from "react";
import "./App.css";
import DrivingSchoolGrid from "./components/DrivingSchoolGrid";
import SearchBar from "./components/SearchBar";

function App() {
    const [searchInput, setSearchInput] = useState("");

    const handleSearchChange = (searchText: string) => {
        setSearchInput(searchText);
    };

    return (
        <>
            <div className="flex flex-wrap gap-10">
                <SearchBar onSearch={handleSearchChange} />
                <DrivingSchoolGrid searchInput={searchInput} />
            </div>
        </>
    );
}

export default App;
