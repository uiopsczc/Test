--https://www.cnblogs.com/LiaHon/p/11203229.html
--节点的左子树小于节点本身；
--节点的右子树大于节点本身；
--左右子树同样为二叉搜索树；
---@class RBTree
local RBTree = class("RBTree")

---@class RBTreeNode
---@field cmp_key number
---@field rb_right RBTreeNode
---@field rb_left RBTreeNode
---@field rb_parent RBTreeNode
---@field rb_color number
---@field data_list RBTreeNodeData[]
local _

---@class RBTreeNodeData
---@field node RBTreeNode
---@param cmp_key number
local __

function RBTree:Init()
  self.RB_RED = 0
  self.RB_BLACK = 1
  self.root = {}
  self.node_cnt = 0
  self.data_cnt = 0
end

---@param node RBTreeNode
function RBTree:__rb_rotate_left(node)
  local right = node.rb_right

  node.rb_right = right.rb_left
  if node.rb_right then
    right.rb_left.rb_parent = node
  end
  right.rb_left = node

  right.rb_parent = node.rb_parent
  if right.rb_parent then
    if node == node.rb_parent.rb_left then
      node.rb_parent.rb_left = right
    else
      node.rb_parent.rb_right = right
    end
  else
    self.root.rb_node = right
  end

  node.rb_parent = right
end

---@param node RBTreeNode
function RBTree:__rb_rotate_right(node)
  local left = node.rb_left

  node.rb_left = left.rb_right
  if node.rb_left then
    left.rb_right.rb_parent = node
  end
  left.rb_right = node

  left.rb_parent = node.rb_parent
  if left.rb_parent then
    if node == node.rb_parent.rb_right then
      node.rb_parent.rb_right = left
    else
      node.rb_parent.rb_left = left
    end
  else
    self.root.rb_node = left
  end

  node.rb_parent = left
end

---@param node RBTreeNode
function RBTree:rb_insert_color(node)
  local parent, gparent
  parent = node.rb_parent
  while parent and (parent.rb_color == self.RB_RED) do
    while true do ---- 这个循环是为了改一个continue

    gparent = parent.rb_parent

    if parent == gparent.rb_left then
      local uncle = gparent.rb_right
      if uncle and (uncle.rb_color == self.RB_RED) then
        uncle.rb_color = self.RB_BLACK
        parent.rb_color = self.RB_BLACK
        gparent.rb_color = self.RB_RED
        node = gparent
        break
      end

      if parent.rb_right == node then
        local tmp
        self:__rb_rotate_left(parent)
        tmp = parent
        parent = node
        node = tmp
      end

      parent.rb_color = self.RB_BLACK
      gparent.rb_color = self.RB_RED
      self:__rb_rotate_right(gparent)
    else
      local uncle = gparent.rb_left
      if uncle and (uncle.rb_color == self.RB_RED) then
        uncle.rb_color = self.RB_BLACK
        parent.rb_color = self.RB_BLACK
        gparent.rb_color = self.RB_RED
        node = gparent
        break
      end

      if parent.rb_left == node then
        local tmp
        self:__rb_rotate_right(parent)
        tmp = parent
        parent = node
        node = tmp
      end
      parent.rb_color = self.RB_BLACK
      gparent.rb_color = self.RB_RED
      self:__rb_rotate_left(gparent)
    end

    break---- 这个循环是为了改一个continue
    end---- 这个循环是为了改一个continue

    parent = node.rb_parent
  end

  self:RootNode().rb_color = self.RB_BLACK
end

---@param node RBTreeNode
function RBTree:__rb_erase_color(node, parent)
  local other

  while ((not node) or (node.rb_color == self.RB_BLACK)) and (node ~= self:RootNode()) do
    if parent.rb_left == node then
      other = parent.rb_right
      if other.rb_color == self.RB_RED then
        other.rb_color = self.RB_BLACK
        parent.rb_color = self.RB_RED
        self:__rb_rotate_left(parent)
        other = parent.rb_right
      end
      if ((not other.rb_left or other.rb_left.rb_color == self.RB_BLACK) and (not other.rb_right or other.rb_right.rb_color == self.RB_BLACK)) then
        other.rb_color = self.RB_RED
        node = parent
        parent = node.rb_parent
      else
        if (not other.rb_right) or (other.rb_right.rb_color == self.RB_BLACK) then
          local o_left = other.rb_left
          if o_left then
            o_left.rb_color = self.RB_BLACK
          end
          other.rb_color = self.RB_RED
          self:__rb_rotate_right(other)
          other = parent.rb_right
        end
        other.rb_color = parent.rb_color
        parent.rb_color = self.RB_BLACK
        if other.rb_right then
          other.rb_right.rb_color = self.RB_BLACK
        end
        self:__rb_rotate_left(parent)
        node = self:RootNode()
        break
      end
    else
      other = parent.rb_left
      if other.rb_color == self.RB_RED then
        other.rb_color = self.RB_BLACK
        parent.rb_color = self.RB_RED
        self:__rb_rotate_right(parent)
        other = parent.rb_left
      end
      if ((not other.rb_left or other.rb_left.rb_color == self.RB_BLACK) and (not other.rb_right or other.rb_right.rb_color == self.RB_BLACK)) then
        other.rb_color = self.RB_RED
        node = parent
        parent = node.rb_parent
      else
        if (not other.rb_left or other.rb_left.rb_color == self.RB_BLACK) then
          local o_right = other.rb_right
          if o_right then
            o_right.rb_color = self.RB_BLACK
          end
          other.rb_color = self.RB_RED
          self:__rb_rotate_left(other)
          other = parent.rb_left
        end
        other.rb_color = parent.rb_color
        parent.rb_color = self.RB_BLACK
        if other.rb_left then
          other.rb_left.rb_color = self.RB_BLACK
        end
        self:__rb_rotate_right(parent)
        node = self:RootNode()
        break
      end
    end
  end
  if node then
    node.rb_color = self.RB_BLACK
  end
end

---@param node RBTreeNode
---@param parent_node RBTreeNode
---@param hand_key string
function RBTree:__insert_new_node(new_node, parent_node, hand_key)
  new_node.rb_parent = parent_node
  new_node.rb_color = self.RB_RED
  if parent_node == nil then
    self.root.rb_node = new_node
  else
    parent_node[hand_key] = new_node
  end

  self:rb_insert_color(new_node)

  self.node_cnt = self.node_cnt + 1
end


-- 是否将node排除在外
-- exclude_node_func  排除条件
---@param node RBTreeNode
---@param exclude_node_func fun(node:RBTreeNode):boolean
function RBTree.__IsExcludeNode(node, exclude_node_func)
  if not exclude_node_func then
    return false
  end
  return exclude_node_func(node)
end

-- 是否将data排除在外
-- exclude_data_func  排除条件
---@param data RBTreeNodeData
---@param exclude_data_func fun(data:RBTreeNodeData):boolean
function RBTree.__IsExcludeData(data, exclude_data_func)
  if not exclude_data_func then
    return false
  end
  return exclude_data_func(data)
end

---@param node RBTreeNode
---@param exclude_data_func fun(data:RBTreeNodeData):boolean
---@param data_list RBTreeNodeData[]
function RBTree.AddToDataListWithExcludeData(node, data_list,  exclude_data_func)
  for _, data in ipairs(node.data_list) do
    if not exclude_data_func and not exclude_data_func(data) then
      table.insert(data_list, data)
    end
  end
end

----------------------------------------------------------------------
-- public外部方法
----------------------------------------------------------------------
-- 删除某个node
---@param node RBTreeNode
function RBTree:Remove(node)
  self.data_cnt = self.data_cnt - #node.data_list

  local child, parent, color

  if not node.rb_left then
    child = node.rb_right
  elseif not node.rb_right then
    child = node.rb_left
  else
    local old = node
    node = node.rb_right
    local left = node.rb_left
    while left ~= nil do
      node = left
      left = node.rb_left
    end
    child = node.rb_right
    parent = node.rb_parent
    color = node.rb_color

    if child then
      child.rb_parent = parent
    end
    if parent then
      if parent.rb_left == node then
        parent.rb_left = child
      else
        parent.rb_right = child
      end
    else
      self.root.rb_node = child
    end

    if node.rb_parent == old then
      parent = node
    end
    local tmp = node
    tmp.rb_parent = old.rb_parent
    tmp.rb_color = old.rb_color
    tmp.rb_right = old.rb_right
    tmp.rb_left = old.rb_left

    if old.rb_parent then
      if old.rb_parent.rb_left == old then
        old.rb_parent.rb_left = node
      else
        old.rb_parent.rb_right = node
      end
    else
      self.root.rb_node = node
    end

    old.rb_left.rb_parent = node
    if old.rb_right then
      old.rb_right.rb_parent = node
    end
    --goto color
    if color == self.RB_BLACK then
      self:__rb_erase_color(child, parent)
    end
    self.node_cnt = self.node_cnt - 1
    return
  end

  parent = node.rb_parent
  color = node.rb_color

  if child then
    child.rb_parent = parent
  end
  if parent then
    if parent.rb_left == node then
      parent.rb_left = child
    else
      parent.rb_right = child
    end
  else
    self.root.rb_node = child
  end

--color:
  if color == self.RB_BLACK then
    self:__rb_erase_color(child, parent)
  end
  self.node_cnt = self.node_cnt - 1
end

function RBTree:RemoveByCmpValue(cmp_value)
  local node = self:FindNode(cmp_value)
  if node then
    self:Remove(node)
  end
end

---@param data RBTreeNodeData
function RBTree:RemoveData(data)
  if #data.node.data_list == 1 then
    self:Remove(data.node)
  else
    table.RemoveByValue_Array(data)
  end
end

---最顶层的Node
---@return RBTreeNode
function RBTree:RootNode()
  return self.root.rb_node
end

--获取第一个node
---@return RBTreeNode
function RBTree:FirstNode()
  local n = self:RootNode()
  if not n then
    return nil
  end
  while n.rb_left do
    n = n.rb_left
  end
  return n
end

--最后一个Node
---@return RBTreeNode
function RBTree:LastNode()
  local n = self:RootNode()
  if not n then
    return nil
  end
  while n.rb_right ~= nil do
    n = n.rb_right
  end
  return n
end

--获取下一个node
---@param node RBTreeNode
---@return RBTreeNode
function RBTree:NextNode(node)
  if (node.rb_right) then
    node = node.rb_right
    while (node.rb_left) do
      node = node.rb_left
    end
    return node
  end

  while (node.rb_parent and node == node.rb_parent.rb_right) do
    node = node.rb_parent
  end

  return node.rb_parent
end

--获取下一个node，带过滤函数的
---@param node RBTreeNode
---@param exclude_node_func fun(node:RBTreeNode):boolean
---@return RBTreeNode
function RBTree:NextNodeWithExculde(node, exclude_node_func)
  while true do
    local next_node = self:NextNode(node)
    if not next_node then
      return nil
    end
    if not RBTree.__IsExcludeNode(next_node, exclude_node_func) then
      return next_node
    end
    node = next_node
  end
end



--上一个Node
---@param node RBTreeNode
---@return RBTreeNode
function RBTree:PrevNode(node)
  if node.rb_left then
    node = node.rb_left
    while node.rb_right ~= nil do
      node = node.rb_right
    end
    return node
  end

  while (node.rb_parent and node == node.rb_parent.rb_left) do
    node = node.rb_parent
  end

  return node.rb_parent
end

--上一个Node，带过滤函数的
---@param node RBTreeNode
---@param exclude_node_func fun(node:RBTreeNode):boolean
---@return RBTreeNode
function RBTree:PrevNodeWithExculde(node, exclude_node_func)
  while true do
    local prev_node = self:PrevNode(node)
    if not prev_node then
      return nil
    end
    if not RBTree.__IsExcludeNode(prev_node, exclude_node_func) then
      return prev_node
    end
    node = prev_node
  end
end

-- 找一个与compare_value最接近的节点
-- compare_value 比较的值
-- exclude_node_func 排除节点的方法
---@param compare_value number
---@param exclude_node_func fun(node:RBTreeNode):boolean
---@return RBTreeNode
function RBTree:GetNearestNode(compare_value, exclude_node_func)
  -- 如果红黑树为nil或者红黑树中没有节点，则返回nil
  if self.node_cnt == 0 then
    return nil
  end

  local prev_diff = math.huge  -- 上个符合条件的节点与compare_value的差值
  local prev_node = nil --上一个节点
  local cur_node =  self:NextNodeWithExculde(self:FirstNode(), exclude_node_func)  -- 当前的node
  if not cur_node then
    return nil
  end

  -- 从最小cmp_key的节点开始比较
  while true do
    -- 当前节点的值与compare_value的差值
    local cur_diff = math.abs(cur_node.cmp_key - compare_value)
    if cur_diff > prev_diff then
      return prev_node
    end
    -- 找下一个符合条件的node
    local next_node = self:NextNodeWithExculde( cur_node, exclude_node_func)
    if not next_node then
      break
    end
    prev_node = cur_node
    prev_diff = cur_diff -- 设置上个符合条件的节点与compare_value的差值
    cur_node = next_node
  end
  return cur_node
end


-- 找一个与compare_value最接近的节点列表,最少min_count个
-- compare_value 比较的值
-- exclude_node_func 排除节点的方法
---@param compare_value number
---@param exclude_node_func fun(node:RBTreeNode):boolean
---@return RBTreeNode
function RBTree:GetNearestNodeList(compare_value, min_count, exclude_node_func)
  local node_list = {}
  local nearest_node = self:GetNearestNode(compare_value, exclude_node_func) -- 找最符合的节点
  if not nearest_node then
    return node_list
  end
  table.insert(node_list, nearest_node)
  if #node_list >= min_count then
    return node_list
  end

  local prev_node = self:PrevNodeWithExculde(nearest_node, exclude_node_func) --前一个节点
  local next_node = self:NextNodeWithExculde(nearest_node, exclude_node_func) --后一个节点
  if not prev_node and not next_node then -- 是叶子节点的话，结束
    return node_list
  end

  local last_diff
  local AddMinDiffNodeFunc = function(compare_last_diff)
    --优先cmp_key值小的处理
    if prev_node and not next_node -- 有前一个节点，没有后一个节点的情况
        or math.abs(prev_node.cmp_key - compare_value) <= compare_last_diff -- 前一节点的diff比后一个节点的diff小
    then
      last_diff = math.abs(prev_node.cmp_key - compare_value)
      table.insert(node_list, prev_node)
      prev_node = self:PrevNodeWithExculde(prev_node, exclude_node_func)
    else
      last_diff = math.abs(next_node.cmp_key - compare_value)
      table.insert(node_list, next_node)
      next_node = self:NextNodeWithExculde(next_node, exclude_node_func)
    end
  end

  AddMinDiffNodeFunc(math.abs(next_node.cmp_key - compare_value))
  if #node_list >= min_count then
    return node_list
  end

  while next_node or prev_node do
    AddMinDiffNodeFunc(last_diff)
    if #node_list >= min_count then
      return node_list
    end
  end

  return node_list
end


-- 找一个与compare_value最接近的data列表,最少min_count个
-- compare_value 比较的值
-- exclude_node_func 排除节点的方法
-- exclude_data_func 排除节点的方法
---@param compare_value number
---@param exclude_node_func fun(node:RBTreeNode):boolean
---@param exclude_data_func fun(data:RBTreeNodeData):boolean
---@return RBTreeNode
function RBTree:GetNearestDataList(compare_value, min_count, exclude_node_func, exclude_data_func)
  local data_list = {}
  local nearest_node = self:GetNearestNode(compare_value, exclude_node_func) -- 找最符合的节点
  if not nearest_node then
    return data_list
  end

  RBTree.AddToDataListWithExcludeData(nearest_node, data_list, exclude_data_func)
  if #data_list >= min_count then
    return data_list
  end

  local prev_node = self:PrevNodeWithExculde(nearest_node, exclude_node_func) --前一个节点
  local next_node = self:NextNodeWithExculde(nearest_node, exclude_node_func) --后一个节点
  if not prev_node and not next_node then -- 是叶子节点的话，结束
    return data_list
  end

  local last_diff
  local AddMinDiffNodeDataListFunc = function(compare_last_diff)
    --优先cmp_key值小的处理
    if prev_node and not next_node -- 有前一个节点，没有后一个节点的情况
        or math.abs(prev_node.cmp_key - compare_value) <= compare_last_diff -- 前一节点的diff比后一个节点的diff小
    then
      last_diff = math.abs(prev_node.cmp_key - compare_value)
      RBTree.AddToDataListWithExcludeData(prev_node, data_list, exclude_data_func)
      prev_node = self:PrevNodeWithExculde(prev_node, exclude_node_func)
    else
      last_diff = math.abs(next_node.cmp_key - compare_value)
      RBTree.AddToDataListWithExcludeData(next_node, data_list, exclude_data_func)
      next_node = self:NextNodeWithExculde(next_node, exclude_node_func)
    end
  end

  AddMinDiffNodeDataListFunc(math.abs(next_node.cmp_key - compare_value))
  if #data_list >= min_count then
    return data_list
  end

  while next_node or prev_node do
    AddMinDiffNodeDataListFunc(last_diff)
    if #data_list >= min_count then
      return data_list
    end
  end
  return data_list
end




-- 这个insert函数支持相等的cmp_key以列表形式存储在同一个结点，所以并不要求cmp_key不同
---@param cmp_key number
---@param data RBTreeNodeData
function RBTree:Insert(cmp_key, data)
  assert(cmp_key)
  assert(data)
  data.cmp_key = cmp_key

  self.data_cnt = self.data_cnt + 1

  local p_node = self:RootNode()
  local parent = nil
  local hand_key = ""

  while (p_node ~= nil) do
    parent = p_node

    if cmp_key < p_node.cmp_key then
      p_node = p_node.rb_left
      hand_key = "rb_left"
    else
      if cmp_key > p_node.cmp_key then
        p_node = p_node.rb_right
        hand_key = "rb_right"
      else
        -- 相等插入列表
        if cmp_key == p_node.cmp_key then
          data.node = p_node
          table.insert(p_node.data_list, data)
          return
        end
      end
    end
  end

  local new_node = {
    cmp_key = cmp_key,
    data_list = { data },
  }
  data.node = new_node
  self:__insert_new_node(new_node, parent, hand_key)
end

-- 设置data，如果没有cmp_key的节点，则创建含data的节点则插入，如果有则覆盖
---@param cmp_key number
---@param data RBTreeNodeData
function RBTree:SetNodeData(cmp_key, data)
  assert(cmp_key)
  assert(data)

  data.cmp_key = cmp_key
  local p_node = self:RootNode()
  local parent = nil
  local hand_key = ""

  while (p_node ~= nil) do
    parent = p_node

    if cmp_key < p_node.cmp_key then
      p_node = p_node.rb_left
      hand_key = "rb_left"
    else
      if cmp_key > p_node.cmp_key then
        p_node = p_node.rb_right
        hand_key = "rb_right"
      else
        -- 相等插入列表
        if cmp_key == p_node.cmp_key then
          self.data_cnt = self.data_cnt - #p_node.data_list + 1
          data.node = p_node
          p_node.data_list = { data }
          return
        end
      end
    end
  end

  self.data_cnt = self.data_cnt + 1
  local new_node = {
    cmp_key = cmp_key,
    data_list = { data },
  }
  data.node = new_node
  self:__insert_new_node(new_node,parent,hand_key)
end

-- 查找指定值的节点
---@return RBTreeNode
---@param cmp_key number
function RBTree:FindNode(cmp_key)
  local p_node = self:RootNode()

  while (p_node ~= nil) do
    if cmp_key < p_node.cmp_key then
      p_node = p_node.rb_left
    else
      if cmp_key > p_node.cmp_key then
        p_node = p_node.rb_right
      else
        -- 相等
        if cmp_key == p_node.cmp_key then
          return p_node
        end
      end
    end
  end
  return nil
end

---@return RBTreeNode
---@param cmp_key number
---@param exclude_node_func fun(node:RBTreeNode):boolean
function RBTree:FindNodeWithExclude(cmp_key, exclude_node_func)
  local node = self:FindNode(cmp_key)
  if node and  not RBTree.__IsExcludeNode(node, exclude_node_func) then
    return node
  else
    return nil
  end
end

-- 返回第一个大于或等于的节点
---@return RBTreeNode
---@param cmp_key number
function RBTree:FindNodeGE(cmp_key)
  local p_node = self:RootNode()
  local last_note = nil
  while (p_node ~= nil) do
    --log("p_node.cmp_key", p_node.cmp_key, cmp_key)
    if p_node.cmp_key > cmp_key then --节点当前值比（要比较的值）大，则找当前节点更小的节点（即左节点）
      last_note = p_node
      p_node = p_node.rb_left -- 当没有子节点的时候，last_note就是要找的节点
    elseif p_node.cmp_key < cmp_key then --节点当前值比（要比较的值）小，则找更大的节点（即右节点）
      p_node = p_node.rb_right
    else
      return p_node
    end
  end
  return last_note
end


---@return RBTreeNode
---@param cmp_key number
---@param exclude_node_func fun(node:RBTreeNode):boolean
function RBTree:FindNodeGEWithExclude(cmp_key, exclude_node_func)
  local node = self:FindNodeGE(cmp_key)
  if not node then
    return
  end
  if not RBTree.__IsExcludeNode(node, exclude_node_func) then
    return node
  else
    --排除在外的情况，继续找更大的节点
    return self:NextNodeWithExculde(node, exclude_node_func)
  end
end


-- 返回第一个少于或等于的节点
---@return RBTreeNode
---@param cmp_key number
function RBTree:FindNodeLE(cmp_key)
  local p_node = self:RootNode()
  local last_note = nil
  while (p_node ~= nil) do
    --log("p_node.cmp_key", p_node.cmp_key, cmp_key)
    if p_node.cmp_key < cmp_key then --节点当前值比（要比较的值）小，则找当前节点更大的节点（即右节点）
      last_note = p_node
      p_node = p_node.rb_right
    elseif p_node.cmp_key > cmp_key then  --节点当前值比（要比较的值）大，则找更小的节点（即左节点）
      p_node = p_node.rb_left
    else
      return p_node
    end
  end
  return last_note
end

---@return RBTreeNode
---@param cmp_key number
---@param exclude_node_func fun(node:RBTreeNode):boolean
function RBTree:FindNodeLEWithExclude(cmp_key, exclude_node_func)
  local node = self:FindNodeLE(cmp_key)
  if not node then
    return
  end
  if not RBTree.__IsExcludeNode(node, exclude_node_func) then
    return node
  else
    --排除在外的情况，继续找更小的节点
    return self:PrevNodeWithExculde(node, exclude_node_func)
  end
end

-- 小于等于cmp_key的全部节点拿出来并把相关结点删掉
---@return RBTreeNodeData[]
---@param cmp_key number
function RBTree:RemoveAllLE(cmp_key)
  local data_list = {}
  while true do
    local node = self:FirstNode()
    if node == nil then
      break
    end
    if node.cmp_key <= cmp_key then
      for i,v in ipairs(node.data_list) do
        table.insert(data_list, v)
      end
      self:Remove(node)
    else
      break
    end
  end
  return data_list
end

-- 大于等于cmp_key的全部节点拿出来并把相关结点删掉
---@return RBTreeNodeData[]
---@param cmp_key number
function RBTree:RemoveAllGE(cmp_key)
  local data_list = {}
  while true do
    local node = self:LastNode()
    if node == nil then
      break
    end
    if node.cmp_key >= cmp_key then
      for i,v in ipairs(node.data_list) do
        table.insert(data_list, v)
      end
      self:Remove(node)
    else
      break
    end
  end
  return data_list
end

return RBTree