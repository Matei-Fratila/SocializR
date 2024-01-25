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
}

export type Posts = Array<Post>;

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

export type PostsListProps = {
    posts: Posts;
    // onRemoveItem: (item: Post) => void;
}

export type PostProps = {
    item: Post;
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