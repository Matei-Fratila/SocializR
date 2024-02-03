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
    roles: [Role]
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
    userPhoto: string;
    relationToCurrentUser: RelationType;
    firstName: string;
    lastName: string;
    birthDate: Date;
    city: SelectItem;
    county: SelectItem;
    gender: string;
    isPrivate: boolean;
    nrOfFriends: number;
    nrOfPosts: number;
    nrOfPhotos: number;
    mutualFriends: number;
    description: string;
    filePath: string;
    interests: SelectItem[];
    albums: Album[];
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
    createdDate: string;
    media: Media[];
}

export type Media = {
    id: string;
    albumId: string;
    type: MediaType;
    caption: string;
    createdDate: string;
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