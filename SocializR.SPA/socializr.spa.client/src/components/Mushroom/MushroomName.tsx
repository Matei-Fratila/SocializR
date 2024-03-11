interface MushroomNameProps {
    denumire: string,
    denumirePopulara: string,
}

const MushroomName = ({denumire, denumirePopulara} : MushroomNameProps) => {
    return (<>
        {denumirePopulara && `${denumirePopulara} (`}
        <span className="fst-italic">{denumire}</span>
        {denumirePopulara && `)`}
        {" "}
    </>);
};

export default MushroomName;