import { SelectItem } from "../types/types";
import React from "react";
import profileService from "../services/profile.service";
import { Button, Col, Form, Row } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import locationService from "../services/selectItems.service";
import selectItemsService from "../services/selectItems.service";
import { PencilFill } from "react-bootstrap-icons";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { Avatar, TextareaAutosize } from "@mui/material";
import Select from "react-select";
import axiosInstance from '../helpers/axios-helper';

export type ProfileForm = {
    id: string;
    avatar: string;
    newAvatar: File[];
    firstName: string;
    lastName: string;
    birthDate: string;
    city: SelectItem | null;
    county: SelectItem;
    gender: SelectItem;
    isPrivate: boolean;
    description: string;
    interests: SelectItem[];
}

const defaultProfile = {
    id: "",
    avatar: "",
    newAvatar: [],
    firstName: "",
    lastName: "",
    birthDate: "",
    city: { label: "", value: "" },
    county: { label: "", value: "" },
    gender: { label: "", value: "" },
    isPrivate: false,
    description: "",
    interests: [] as SelectItem[]
};

const ProfileEdit = () => {
    const navigate = useNavigate();
    const { id } = useParams();

    const { control, register, handleSubmit, watch, formState: { errors }, resetField } = useForm<ProfileForm>({
        defaultValues: async () => {
            try {
                if (id !== undefined) {
                    const profile: ProfileForm = await profileService.getProfileFormAsync(id);
                    profile.birthDate = profile.birthDate.split("T")[0];
                    return profile;
                }
            } catch (error) {
                console.error(error);
            };
            return defaultProfile;
        }
    });

    const watchInfo = watch(["county", "newAvatar", "avatar", "birthDate"]);

    const [genderOptions, setGenders] = React.useState([] as SelectItem[]);
    const [countyOptions, setCounties] = React.useState([] as SelectItem[]);
    const [cityOptions, setCities] = React.useState([] as SelectItem[]);
    const [interestOptions, setInterests] = React.useState([] as SelectItem[]);

    const handleFetchCities = async () => {
        try {
            const citiesResult = await locationService.getCities(watchInfo[0].value);
            setCities(citiesResult);
        } catch {

        }
    }

    const handleFetchCounties = async () => {
        try {
            const countiesResult = await locationService.getCounties();
            setCounties(countiesResult);
        } catch {

        }
    }

    const handleFetchInterests = async () => {
        try {
            const interestsResult = await selectItemsService.getInterests();
            setInterests(interestsResult);
        } catch {

        }
    }

    const handleFetchGenders = async () => {
        try {
            const gendersResult = await selectItemsService.getGenders();
            setGenders(gendersResult);
        } catch {

        }
    }

    React.useEffect(() => {
        handleFetchCounties();
        handleFetchInterests();
        handleFetchGenders();
    }, []);

    React.useEffect(() => {
        if (watchInfo[0] !== null) {
            handleFetchCities();
        }
    }, [watchInfo[0]]);

    const onSubmit: SubmitHandler<ProfileForm> = async (data) => {
        try {
            await profileService.editProfileAsync({...data, avatar: data.newAvatar[0]});
            navigate(`/profile/${id}`);
        } catch (error) {
            console.error(error)
        }
    };

    return (
        <Row>
            <Col>
                <h5><PencilFill /> Editare Profil</h5>
                <hr />
                <Form onSubmit={handleSubmit(onSubmit)}>

                    <Avatar className="mb-3"
                        src={(watchInfo[1] !== undefined && watchInfo[1].length !== 0) ? URL.createObjectURL(watchInfo[1][0]) : `${axiosInstance.defaults.baseURL}${watchInfo[2]}`}
                        sx={{ width: '7em', height: '7em' }} />

                    <Form.Group className="mb-3" controlId="descriere">
                        <Form.Label>Avatar</Form.Label>
                        <Form.Control className="form-control" type="file" accept="image/*" {...register("newAvatar")} />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="descriere">
                        <Form.Label>Descriere</Form.Label>
                        <TextareaAutosize className="form-control" {...register("description")} />
                        <Form.Control.Feedback type="invalid" className='d-block'>
                            {errors.description?.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="firstName">
                        <Form.Label>Prenume *</Form.Label>
                        <Form.Control isInvalid={errors.firstName !== undefined} type="text" {...register("firstName", { required: "Câmp obligatoriu" })} />
                        <Form.Control.Feedback type="invalid" className='d-block'>
                            {errors.firstName?.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="lastName">
                        <Form.Label>Nume *</Form.Label>
                        <Form.Control isInvalid={errors.lastName !== undefined} type="text" {...register("lastName", { required: "Câmp obligatoriu" })} />
                        <Form.Control.Feedback type="invalid" className='d-block'>
                            {errors.lastName?.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="birthDate">
                        <Form.Label>Data nașterii *</Form.Label>
                        <Form.Control isInvalid={errors.birthDate !== undefined} type="date" {...register("birthDate", { required: "Câmp obligatoriu" })} />
                        <Form.Control.Feedback type="invalid" className='d-block'>
                            {errors.birthDate?.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="gender">
                        <Form.Label>Gen *</Form.Label>
                        <Controller
                            name="gender"
                            control={control}
                            render={({ field }) => (
                                <Select
                                    {...field}
                                    options={genderOptions}
                                />
                            )}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="county">
                        <Form.Label>Județ *</Form.Label>
                        <Controller
                            name="county"
                            control={control}
                            render={({ field }) => (
                                <Select
                                    {...field}
                                    options={countyOptions}
                                    onInputChange={() => resetField("city", { defaultValue: null })}
                                    required={true}
                                />
                            )}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="city">
                        <Form.Label>Oraș *</Form.Label>
                        <Controller
                            name="city"
                            control={control}
                            render={({ field }) => (
                                <Select
                                    {...field}
                                    options={cityOptions}
                                    required={true}
                                />
                            )}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="interests">
                        <Form.Label>Interese</Form.Label>
                        <Controller
                            name="interests"
                            control={control}
                            render={({ field }) => (
                                <Select
                                    {...field}
                                    options={interestOptions}
                                    isMulti={true}
                                />
                            )}
                        />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="isPrivate">
                        <Form.Check type="switch" label="Profil privat" {...register("isPrivate")}></Form.Check>
                    </Form.Group>

                    <Button type="submit">Salvare</Button>
                </Form>
            </Col>
        </Row>
    );
}

export default ProfileEdit;