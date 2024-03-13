import React from "react";
import mushroomsService from "../../services/mushrooms.service";
import { Ciuperca, Comestibilitate, MorfologieCorpFructifer } from "../../types/types";
import MushroomName from "./MushroomName";
import { Card, CardBody, CardTitle } from "react-bootstrap";

interface MushroomPopoverProps {
    id: number;
}

const MushroomPopover = ({ id }: MushroomPopoverProps) => {
    
    const [mushroom, setMushroom] = React.useState<Ciuperca>({
        id: 0,
        denumire: "",
        denumirePopulara: "",
        corpulFructifer: "",
        ramurile: "",
        palaria: "",
        piciorul: "",
        stratulHimenial: "",
        gleba: "",
        tuburileSporifere: "",
        lamelele: "",
        carnea: "",
        perioadaDeAparitie: "",
        valoareaAlimentara: "",
        speciiAsemanatoare: "",
        idSpeciiAsemanatoare: [],
        esteMedicinala: false,
        comestibilitate: Comestibilitate.Necunoscuta,
        locDeFructificatie: [],
        morfologieCorpFructifer: MorfologieCorpFructifer.HimenoforNelamelarNetubular,
        luniDeAparitie: [],
        perioada: [],
    });

    async function handleFetchMushroom() {
        try {
            const response: Ciuperca = await mushroomsService.getMushroom(id);
            setMushroom(response);
        } catch (error) {
            console.log(error);
        }
    }

    React.useEffect(() => {
        if (id !== 0) {
            handleFetchMushroom();
        }
    }, [id])

    return (
        <Card style={{width: "15em", height: "17em"}}>
            <Card.Img variant="top"
                src={`${mushroomsService.mushroomsApi.defaults.baseURL}/images/${id}.jpg`} className="px-0">
            </Card.Img>

            <CardBody>
                <CardTitle className="kaki mb-2">
                    <MushroomName {...mushroom}></MushroomName>
                </CardTitle>
            </CardBody>
        </Card>
    );
};

export default MushroomPopover;