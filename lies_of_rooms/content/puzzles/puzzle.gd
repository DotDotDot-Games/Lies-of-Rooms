extends Node2D

class_name Puzzle

signal completed
signal rollbacked

@export var puzzle_name: String
@export var interact_area: InteractArea2D
@export var ui: PackedScene

var _is_completed: bool = false
var is_completed: bool:
	get: return _is_completed
	set(value): return

func _ready() -> void:

	if not interact_area.interacted.is_connected(_on_interacted):
		interact_area.interacted.connect(_on_interacted)

func complete() -> bool:

	if (is_completed):
		return false
	
	_is_completed = true
	completed.emit()
	return true

func rollback() -> bool:

	if (not _is_completed):
		return false
	
	_is_completed = false
	rollbacked.emit()

	return true

func _on_interacted(_node: Node) -> void:
	var ui_node: PuzzleUI = UIManager.open_scene(ui)
	ui_node.puzzle = self
	
	ui_node.tree_exited.connect(_on_close_ui, ConnectFlags.CONNECT_ONE_SHOT)

	get_tree().paused = true

func _on_close_ui() -> void:
	get_tree().paused = false