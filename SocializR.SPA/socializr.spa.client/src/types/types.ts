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
    title: string;
    body: string;
    createdOn: Date;
    userId: string;
    numberOfLikes: number;
    numberOfComments: number;
    isLikedByCurrentUser: boolean;
}

export type Posts = Array<Post>;

export type PostsListProps = {
    posts: Posts;
    // onRemoveItem: (item: Post) => void;
}

export type PostProps = {
    item: Post;
}

export type PostsState = {
    data: Posts;
    page: number;
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

export type PostsAction = PostsFetchAction | PostsFetchSuccessAction | PostsFetchFailureAction;