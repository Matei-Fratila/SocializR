export interface LoginRequest {
    email: string,
    password: string
}

export interface LoginResponse {
    token: string,
    currentUser: CurrentUser
}

export interface RegisterRequest {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    birthDate: Date
}

export interface RegisterResponse {
    token: string,
    currentUser: CurrentUser
}

export interface CurrentUser {
    id: string,
    firstName: string,
    lastName: string,
    profilePhoto: string,
    roles: Role[]
}

export interface Role {
    name: string,
    description: string
}

export type Post = {
    id: string;
    firstName: string;
    lastName: string;
    userPhoto: string;
    title: string;
    body: string;
    createdOn: string;
    userId: string;
    numberOfLikes: number;
    numberOfComments: number;
    isLikedByCurrentUser: boolean;
    comments: Comments;
    media: Media[];
}

export type Posts = Array<Post>;

export type NewPost = {
    title: string;
    body: string;
}

export type PostFormProps = {
    onSubmit: (post: Post) => void;
}

export type Comment = {
    id: string;
    userId: string;
    firstName: string;
    lastName: string;
    userPhoto: string;
    createdOn: string;
    body: string;
    isCurrentUserComment: boolean;
}

export type Comments = Array<Comment>;

export type Profile = {
    id: string;
    avatar: string;
    relationToCurrentUser: RelationType;
    firstName: string;
    lastName: string;
    birthDate: Date;
    city: SelectItem;
    county: SelectItem;
    gender: SelectItem;
    isPrivate: boolean;
    nrOfFriends: number;
    nrOfPosts: number;
    nrOfPhotos: number;
    mutualFriends: number;
    description: string;
    interests: SelectItem[];
    albums: Album[];
}

export type ProfileForm = {
    id: string;
    avatar: File;
    firstName: string;
    lastName: string;
    birthDate: Date;
    city: SelectItem;
    county: SelectItem;
    gender: SelectItem;
    isPrivate: boolean;
    description: string;
    interests: SelectItem[];
}

export type ProfileProps = {
    profile: Profile;
}

export type SelectItem = {
    label: string;
    value: string;
}

export type Album = {
    id: string;
    userId: string;
    name: string;
    description: string;
    coverId: string;
    coverFilePath: string;
    nrOfImages: number;
    createdDate: Date;
    createdDateDisplay: string;
    lastModifiedDate: Date;
    lastModifiedDateDisplay: string;
    media: Media[];
}

export type Media = {
    id: string;
    albumId: string;
    type: MediaType;
    caption: string;
    createdDate: Date;
    createdDateDisplay: string;
    lastModifiedDate: Date;
    lastModifiedDateDisplay: string;
    fileName: string;
}

export type AlbumProps = {
    item: Album;
    onDelete: (album: Album) => void;
}

export type PostsListProps = {
    userId: string;
}

export type PostProps = {
    item: Post;
    onRemoveItem: (id: string) => void;
}

export type CommentProps = {
    item: Comment;
    onRemoveItem: (id: string) => void;
}

export type CommentFormProps = {
    postId: string;
    onSubmit: (comment: Comment) => void;
}

export type PostsState = {
    data: Posts;
    pageNumber: number;
    isLoading: boolean;
    isError: boolean;
}

export type User = {
    id: string;
    profilePhoto: string;
    firstName: string;
    lastName: string;
}

export type Ciuperca = {
    id: number;
    denumire: string;
    denumirePopulara: string;
    corpulFructifer: string;
    ramurile: string;
    palaria: string;
    stratulHimenial: string;
    gleba: string;
    tuburileSporifere: string;
    lamelele: string;
    piciorul: string;
    carnea: string;
    perioadaDeAparitie: string;
    valoareaAlimentara: string;
    speciiAsemanatoare: string;
    idSpeciiAsemanatoare: number[];
    esteMedicinala: boolean;
    comestibilitate: Comestibilitate;
    locDeFructificatie: LocDeFructificatie[];
    morfologieCorpFructifer: MorfologieCorpFructifer;
    perioada: number[];
    luniDeAparitie: Luna[];
}

export type CiupercaEdit = {
    id: number;
    denumire: string;
    denumirePopulara: string;
    corpulFructifer: string;
    ramurile: string;
    palaria: string;
    stratulHimenial: string;
    gleba: string;
    tuburileSporifere: string;
    lamelele: string;
    piciorul: string;
    carnea: string;
    perioadaDeAparitie: string;
    valoareaAlimentara: string;
    speciiAsemanatoare: string;
    idSpeciiAsemanatoare: CiupercaOption[];
    esteMedicinala: boolean;
    comestibilitate: ComestibilitateOption;
    locDeFructificatie: LocDeFructificatieOption[];
    morfologieCorpFructifer: MorfologieCorpFructiferOption;
    perioadaStart: LunaOption;    
    perioadaEnd: LunaOption;
    luniDeAparitie: Luna[];
}

export type ComestibilitateOption = {
    label: string;
    value: Comestibilitate
}

export type LunaOption = {
    label: string;
    value: number
}

export type LocDeFructificatieOption = {
    label: string;
    value: LocDeFructificatie
}

export type MorfologieCorpFructiferOption = {
    label: string;
    value: MorfologieCorpFructifer
}

interface PostsFetchAction {
    type: 'POSTS_FETCH';
}

interface PostsFetchSuccessAction {
    type: 'POSTS_FETCH_SUCCESS';
    payload: Posts;
}

interface PostsFetchFailureAction {
    type: 'POSTS_FETCH_FAILURE';
}

interface NewPostAction {
    type: 'NEW_POST';
    payload: Post;
}

interface DeletePostAction {
    type: 'DELETE_POST';
    payload: string;
}

interface IncreasePageNumberAction {
    type: 'INCREASE_PAGE_NUMBER';
}

export type PostsAction = PostsFetchAction
    | PostsFetchSuccessAction
    | PostsFetchFailureAction
    | NewPostAction
    | DeletePostAction
    | IncreasePageNumberAction;

export enum RelationType {
    Unknown,
    Strangers,
    Friends,
    RequestedFriendship,
    PendingAccept,
    Blocked
}

export enum MediaType {
    Unspecified,
    Image,
    Video
}

export enum Comestibilitate {
    Necunoscuta = "Necunoscută",
    Comestibila = "Comestibilă",
    ConditionatComestibila = "CondiționatComestibilă",
    Necomestibila = "Necomestibilă",
    Otravitoare = "Otrăvitoare"
}

export enum MorfologieCorpFructifer {
    HimenoforNelamelarNetubular = "HimenoforNelamelarNetubular",
    HimenoforTubular = "HimenoforTubular",
    HimenoforLamelar = "HimenoforLamelar"
}

export enum LocDeFructificatie {
    PadureFoioase = "PădureDeFoioase",
    PadureConifere = "PădureDeConifere",
    Pasune = "Pășune",
    CrengiSiCioate = "CrengiȘiCioate"
}

export enum Luna {
    ianuarie = "ianuarie",
    februarie = "februarie",
    martie = "martie",
    aprilie = "aprilie",
    mai = "mai",
    iunie = "iunie",
    iulie = "iulie",
    august = "august",
    septembrie = "septembrie",
    octombrie = "octombrie",
    noiembrie = "noiembrie",
    decembrie = "decembrie"
}

export type CiupercaSearchResult = {
    id: number,
    nume: string
}

export type CiupercaPaginatedResult = {
    totalCount: number,
    ciuperci: Ciuperca[]
}

export type CiupercaOption = {
    label: string,
    value: number
}

export type Filters = {
    esteInSezon: boolean,
    luniDeAparitie: LunaOption[],
    morfologieCorpFructifer: MorfologieCorpFructiferOption[],
    locDeFructificatie: LocDeFructificatieOption[],
    comestibilitate: ComestibilitateOption[],
    esteMedicinala: boolean,
    idSpeciiAsemanatoare: CiupercaOption[],
    gen: SelectItem[],
    sortareDupa: SelectItem,
    ordine: string
}