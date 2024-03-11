import './Mushroom.css';
import { Button, Card, CardBody, Form, Row } from "react-bootstrap";
import { Ciuperca, CiupercaEdit, CiupercaOption, Comestibilitate, ComestibilitateOption, LocDeFructificatie, LocDeFructificatieOption, Luna, LunaOption, MorfologieCorpFructifer, MorfologieCorpFructiferOption } from "../../types/types";
import mushroomsService from "../../services/mushrooms.service";
import { useNavigate, useParams } from "react-router-dom";
import MushroomInfo from "./MushroomInfo";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import Select from 'react-select';
import splitCamelCase from '../../helpers/string-helper';
import { TextareaAutosize } from '@mui/material';
import React from 'react';
import Calendar from './Calendar';

const optionsLocDeFructificatie: LocDeFructificatieOption[] = Object.values(LocDeFructificatie).map(loc => ({ value: loc, label: splitCamelCase(loc) }));
const optionsMorfologieCorpFructifer: MorfologieCorpFructiferOption[] = Object.values(MorfologieCorpFructifer).map(loc => ({ value: loc, label: splitCamelCase(loc) }));
const optionsComestibilitate: ComestibilitateOption[] = Object.values(Comestibilitate).map(loc => ({ value: loc, label: splitCamelCase(loc) }));
const optionsLuni: LunaOption[] = Object.values(Luna).map((value, index) => ({ label: value, value: index + 1 }));

const MushroomEdit = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [mushroomOptions, setMushroomOptions] = React.useState<CiupercaOption[]>([]);

    const { control, register, handleSubmit, watch } = useForm<CiupercaEdit>({
        defaultValues: async () => {
            try {
                const ciuperca = await mushroomsService.getMushroom(Number(id));
                return mushroomsService.mapCiupercaToCiupercaEdit(ciuperca);
            } catch (error) {
                console.error(error);
            };
            return mushroomsService.getDefaultCiupercaEdit();
        }
    });

    const watchInfo = watch(["idSpeciiAsemanatoare", "esteMedicinala", "comestibilitate", "locDeFructificatie", "morfologieCorpFructifer", "perioadaStart", "perioadaEnd"]);

    const onSubmit: SubmitHandler<CiupercaEdit> = async (data) => {
        try {
            const ciuperca: Ciuperca = mushroomsService.mapCiupercaEditToCiuperca(data);
            await mushroomsService.updateMushroom(ciuperca);
            navigate(`/mushrooms/${id}`);

        } catch (error) {
            console.error(error)
        }
    };

    const handleSearchMushrooms = async () => {
        try {
            const mushrooms = await mushroomsService.searchMushrooms("");
            const options = mushrooms.map((mushroom) => mushroomsService.mapCiupercaSearchResultToCiupercaOption(mushroom));
            setMushroomOptions(options);
        } catch {

        }
    }

    React.useEffect(() => {
        handleSearchMushrooms();
    }, [])

    return (
        <Card className='shadow'>
            <Card.Img variant="top" src={`${mushroomsService.mushroomsApi.defaults.baseURL}/images/${id}.jpg`} className="px-0">
            </Card.Img>

            {
                watchInfo[2] && watchInfo[3] && watchInfo[4] &&
                <MushroomInfo
                    id={Number(id)}
                    idSpeciiAsemanatoare={watchInfo[0]?.map((id) => id.value)}
                    esteMedicinala={watchInfo[1]}
                    comestibilitate={watchInfo[2].value}
                    locDeFructificatie={watchInfo[3]?.map((loc) => loc.value)}
                    morfologieCorpFructifer={watchInfo[4]?.value}
                    perioada={[watchInfo[5].value, watchInfo[6].value]}>
                </MushroomInfo>
            }

            <CardBody as={Form} lg={10} md={10} sm={10} xs={12} onSubmit={handleSubmit(onSubmit)}>
                <Form.Group className="mb-3" controlId="denumireStiintifica">
                    <Form.Label>Denumire stiințifică (latină) *</Form.Label>
                    <Form.Control required={true} type="text" {...register("denumire", { required: true })} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="denumirePopulara">
                    <Form.Label>Denumire populară</Form.Label>
                    <Form.Control type="text" {...register("denumirePopulara")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="locDeFructificatie">
                    <Form.Label>Loc de fructificație *</Form.Label>
                    <Controller
                        name="locDeFructificatie"
                        control={control}
                        render={({ field }) => (
                            <Select
                                {...field}
                                options={optionsLocDeFructificatie}
                                isMulti={true}
                                required={true}
                            />
                        )}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="morfologieCorpFructifer">
                    <Form.Label>Morfologia corpului fructifer *</Form.Label>
                    <Controller
                        name="morfologieCorpFructifer"
                        control={control}
                        render={({ field }) => (
                            <Select
                                {...field}
                                options={optionsMorfologieCorpFructifer}
                            />
                        )}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="corpulFructifer">
                    <Form.Label>Corpul fructifer</Form.Label>
                    <TextareaAutosize className="form-control" {...register("corpulFructifer")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="ramurile">
                    <Form.Label>Ramurile</Form.Label>
                    <TextareaAutosize className="form-control" {...register("ramurile")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="palaria">
                    <Form.Label>Pălaria</Form.Label>
                    <TextareaAutosize className="form-control" {...register("palaria")} />
                </Form.Group>

                {
                    watchInfo[4] && watchInfo[4].value === MorfologieCorpFructifer.HimenoforNelamelarNetubular &&
                    <>
                        <Form.Group className="mb-3" controlId="stratulHimenial">
                            <Form.Label>Stratul himenial</Form.Label>
                            <TextareaAutosize className="form-control" {...register("stratulHimenial")} />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="gleba">
                            <Form.Label>Gleba</Form.Label>
                            <TextareaAutosize className="form-control" {...register("gleba")} />
                        </Form.Group>
                    </>
                }

                {
                    watchInfo[4] && (watchInfo[4].value === MorfologieCorpFructifer.HimenoforTubular || watchInfo[4].value === MorfologieCorpFructifer.HimenoforNelamelarNetubular) &&
                    <Form.Group className="mb-3" controlId="tuburileSporifere">
                        <Form.Label>Tuburile sporifere</Form.Label>
                        <TextareaAutosize className="form-control" {...register("tuburileSporifere")} />
                    </Form.Group>
                }

                {
                    watchInfo[4] && watchInfo[4].value === MorfologieCorpFructifer.HimenoforLamelar &&
                    <Form.Group className="mb-3" controlId="lamelele">
                        <Form.Label>Lamelele</Form.Label>
                        <TextareaAutosize className="form-control" {...register("lamelele")} />
                    </Form.Group>
                }

                <Form.Group className="mb-3" controlId="piciorul">
                    <Form.Label>Piciorul</Form.Label>
                    <TextareaAutosize className="form-control" {...register("piciorul")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="carnea">
                    <Form.Label>Carnea *</Form.Label>
                    <TextareaAutosize required={true} className="form-control" {...register("carnea")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="perioadaDeAparitie">
                    <Form.Label>Perioada și locul de apariție *</Form.Label>
                    <TextareaAutosize className="form-control" {...register("perioadaDeAparitie")} />
                </Form.Group>

                <Form.Group className="mb-3 row" controlId="perioada">
                    <Form.Label>Perioada de apariție *</Form.Label>
                    <Controller
                        name="perioadaStart"
                        control={control}
                        render={({ field }) => (
                            <Select
                                className='col-6'
                                {...field}
                                options={optionsLuni}
                                required={true}
                            />
                        )}
                    />
                    <Controller
                        name="perioadaEnd"
                        control={control}
                        render={({ field }) => (
                            <Select
                                className='col-6'
                                {...field}
                                options={optionsLuni}
                                required={true}
                            />
                        )}
                    />
                </Form.Group>

                <Form.Group className="mb-3 row ps-3">
                    <Calendar perioada={[watchInfo[5]?.value, watchInfo[6]?.value]}></Calendar>
                </Form.Group>

                <Form.Group className="mb-3" controlId="comestibilitate">
                    <Form.Label>Comestibilitate *</Form.Label>
                    <Controller
                        name="comestibilitate"
                        control={control}
                        render={({ field }) => (
                            <Select
                                {...field}
                                options={optionsComestibilitate}
                                required={true}
                            />
                        )}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="esteMedicinala">
                    <Form.Check type="switch" label={"Are proprietăți curative"} {...register("esteMedicinala")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="valoareaAlimentara">
                    <Form.Label>Valoarea alimentară *</Form.Label>
                    <TextareaAutosize required={true} className="form-control" {...register("valoareaAlimentara")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="speciiAsemanatoare">
                    <Form.Label>Specii asemănătoare *</Form.Label>
                    <TextareaAutosize required={true} className="form-control" {...register("speciiAsemanatoare")} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="idSpeciiAsemanatoare">
                    <Form.Label>Poate fi confundată cu:</Form.Label>
                    <Controller
                        name="idSpeciiAsemanatoare"
                        control={control}
                        render={({ field }) => (
                            <Select
                                {...field}
                                options={mushroomOptions}
                                isMulti={true}
                            />
                        )}
                    />
                </Form.Group>

                <Button type='submit'>
                    Save
                </Button>
            </CardBody>
            {
                watchInfo[2] && watchInfo[3] && watchInfo[4] &&
                Number(id) % 2 !== 0 &&

                <MushroomInfo
                    id={Number(id)}
                    idSpeciiAsemanatoare={watchInfo[0]?.map((id) => id.value)}
                    esteMedicinala={watchInfo[1]}
                    comestibilitate={watchInfo[2].value}
                    locDeFructificatie={watchInfo[3]?.map((loc) => loc.value)}
                    morfologieCorpFructifer={watchInfo[4]?.value}
                    perioada={[watchInfo[5].value, watchInfo[6].value]}>
                </MushroomInfo>
            }
        </Card>
    )
};

export default MushroomEdit;