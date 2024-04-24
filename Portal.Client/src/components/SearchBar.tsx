import { useRef } from "react";
import { Input } from "./ui/input";

interface Props {
    onSearch: (searchText: string) => void;
}

function SearchBar({ onSearch }: Props) {
    const ref = useRef<HTMLInputElement>(null);

    const handleChange = () => {
        if (ref.current) {
            onSearch(ref.current.value);
        }
    };

    return (
        <>
            <Input ref={ref} placeholder="Søg efter køreskole..." onChange={handleChange} />
        </>
    );
}

export default SearchBar;
