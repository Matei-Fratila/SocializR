import React from "react";
import './Mushroom.css';
import { Button, Card, CardBody, CardText, CardTitle, Col, Row } from "react-bootstrap";
import { Ciuperca, Comestibilitate, MorfologieCorpFructifer } from "../../types/types";
import mushroomsService from "../../services/mushrooms.service";
import { Link, useNavigate, useParams } from "react-router-dom";
import Calendar from "./Calendar";
import MushroomInfo from "./MushroomInfo";
import { ArrowLeft, ArrowRight, PencilFill } from "react-bootstrap-icons";

const Mushroom = () => {
    const { id } = useParams();
    const navigate = useNavigate();

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

    const handleFetchMushroom = React.useCallback(async () => {
        try {
            if (id !== undefined) {
                const result = await mushroomsService.getMushroom(id);
                setMushroom(result);
            }
        }
        catch {
        }
    }, [id]);

    React.useEffect(() => {
        handleFetchMushroom();
    }, [id]);

    return (
        <>
            <Row>
                <Card.Img variant="top" src='../../morells.jpg' className="px-0">
                </Card.Img>

                {Number(id) % 2 === 0 && <MushroomInfo {...mushroom}></MushroomInfo>}

                <Col as={CardBody} lg={10} md={10} sm={10} xs={12} className={`mt-3 ${Number(id) % 2 == 0 ? 'ps-3' : 'pe-3'}`}>
                    <CardTitle className="kaki mb-2">
                        {mushroom?.denumirePopulara
                            ? `${mushroom?.denumirePopulara} (${mushroom?.denumire})`
                            : mushroom.denumire
                        }
                        <Link to={`/mushrooms/edit/${id}`}>
                            <PencilFill></PencilFill>
                        </Link>
                    </CardTitle>
                    {mushroom?.corpulFructifer &&
                        <CardText>
                            <span>
                                Corpul fructifer:{" "}
                            </span>
                            {mushroom.corpulFructifer}
                        </CardText>
                    }
                    {mushroom?.palaria &&
                        <CardText>
                            <span>
                                Pălăria:{" "}
                            </span>
                            {mushroom.palaria}
                        </CardText>
                    }
                    {mushroom?.piciorul &&
                        <CardText>
                            <span>
                                Piciorul:{" "}
                            </span>
                            {mushroom.piciorul}
                        </CardText>
                    }
                    {mushroom?.stratulHimenial &&
                        <CardText>
                            <span>
                                Stratul himenial:{" "}
                            </span>
                            {mushroom.stratulHimenial}
                        </CardText>}
                    {mushroom?.carnea &&
                        <CardText>
                            <span>
                                Carnea:{" "}
                            </span>
                            {mushroom.carnea}
                        </CardText>}
                    {mushroom?.perioadaDeAparitie &&
                        <CardText>
                            <span>
                                Perioada de apariție:{" "}
                            </span>
                            {mushroom.perioadaDeAparitie}
                        </CardText>
                    }
                    {mushroom?.valoareaAlimentara &&
                        <CardText>
                            <span>
                                Valoarea alimentară:{" "}
                            </span>
                            {mushroom.valoareaAlimentara}
                        </CardText>
                    }
                    {mushroom?.speciiAsemanatoare &&
                        <CardText>
                            <span>
                                Specii asemănătoare:{" "}
                            </span>
                            {mushroom.speciiAsemanatoare}

                        </CardText>
                    }
                    <Calendar perioada={mushroom.perioada}></Calendar>
                </Col>

                {Number(id) % 2 === 1 && <MushroomInfo {...mushroom}></MushroomInfo>}

            </Row>

            <Button variant="light" className="float-start" onClick={() => navigate(`/mushrooms/${Number(id) - 1}`)}>
                <ArrowLeft></ArrowLeft>Previous
            </Button>
            <Button variant="light" className="float-end" onClick={() => navigate(`/mushrooms/${Number(id) + 1}`)}>
                Next<ArrowRight></ArrowRight>
            </Button>
        </>
    )
};

export default Mushroom;