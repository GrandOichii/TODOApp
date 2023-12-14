interface Task {
    id: number,
    title: string,
    description: string,
    subtasks: Array<Subtask>
}

interface Subtask {
    id: number,
    title: string,
    completed: boolean
}